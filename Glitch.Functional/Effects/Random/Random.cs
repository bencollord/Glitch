using Glitch.Functional;

namespace Glitch.Functional.Effects;

/// <summary>
/// A monadic effect that allows composing functions over the output
/// of a <see cref="System.Random" /> generator.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Random<T>
{
    private Func<Random> factory;
    private Func<Random, T> generate;

    internal Random(Func<Random, T> generate) : this(CreateDefault, generate) { }

    internal Random(Func<Random> factory, Func<Random, T> generate)
    {
        this.factory = factory;
        this.generate = generate;
    }

    public Random<T> Seed(int seed) => new(() => new Random(seed), generate);

    public Random<IEnumerable<T>> Repeat(int count)
    {
        return new(factory, _ => Iterate());

        IEnumerable<T> Iterate()
        {
            for (int i = 0; i < count; i++)
            {
                yield return Run();
            }
        }
    }

    // Output
    public T Run() => generate(factory());

    public override string ToString() => $"System.Random {typeof(T).Name}";

    // Monad Implementation
    public static Random<T> Return(T value) => new(CreateDefault, _ => value);

    public Random<TResult> Select<TResult>(Func<T, TResult> map) => new(factory, generate >> map);

    public Random<TResult> AndThen<TResult>(Func<T, Random<TResult>> bind)
    {
        return new(factory, rng => 
        {
            var x = generate(rng);
            var y = bind(x);

            return y.generate(rng);
        });
    }

    // Monad boilerplate
    public Random<TResult> AndThen<TElement, TResult>(Func<T, Random<TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(y => project(x, y)));

    public Random<TResult> Apply<TResult>(Random<Func<T, TResult>> apply)
        => apply.AndThen(fn => Select(val => fn(val)));

    public Random<TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

    public Random<TResult> SelectMany<TElement, TResult>(Func<T, Random<TElement>> bind, Func<T, TElement, TResult> project)
       => AndThen(bind, project);

    private static Random CreateDefault() => new();

#pragma warning disable IDE0051 // For LinqPad
    object? ToDump() => Run();
#pragma warning restore IDE0051 // For LinqPad
}
