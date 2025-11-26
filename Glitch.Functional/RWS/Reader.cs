using Glitch.Functional;

namespace Glitch.Functional;

[Monad]
public class Reader<TEnv, T>
{
    private readonly Func<TEnv, T> runner;

    internal Reader(Func<TEnv, T> runner)
    {
        this.runner = runner;
    }

    public static Reader<TEnv, T> Return(T value) => new(_ => value);

    public static Reader<TEnv, T> Lift(Func<T> runner) => new(_ => runner());

    public static Reader<TEnv, T> Asks(Func<TEnv, T> runner) => new(runner);

    /// <summary>
        /// Maps the reader's input value such that the Run method now
        /// takes a new environment type as input.
        /// </summary>
        /// <typeparam name="TNewEnv"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
    public Reader<TNewEnv, T> With<TNewEnv>(Func<TNewEnv, TEnv> map)
        => new(newEnv => Run(map(newEnv)));

    /// <summary>
        /// Returns a new reader that maps its input value before
        /// running the current reader.
        /// </summary>
        /// <remarks>
        /// I'm not sure why the naming convention I've seen in other code uses
        /// Local rather than overloading With for environment mappings that don't
        /// take a type.
        /// </remarks>
        /// <param name="map"></param>
        /// <returns></returns>
    public Reader<TEnv, T> Local(Func<TEnv, TEnv> map)
        => With(map);

    public Reader<TEnv, TResult> Select<TResult>(Func<T, TResult> map)
        => new(env => map(Run(env)));

    public Reader<TEnv, TOther> Then<TOther>(Reader<TEnv, TOther> other)
         => AndThen(_ => other);

    public Reader<TEnv, TResult> Then<TOther, TResult>(Reader<TEnv, TOther> other, Func<T, TOther, TResult> project)
        => AndThen(_ => other, project);

    public Reader<TEnv, T> Then(Reader<TEnv, Unit> other)
         => new(env =>
         {
             var value = Run(env);
             _ = other.Run(env);
             return value;
         });

    public Reader<TEnv, TResult> AndThen<TResult>(Func<T, Reader<TEnv, TResult>> bind)
         => new(env => bind(Run(env)).Run(env));

    public Reader<TEnv, TResult> AndThen<TElement, TResult>(Func<T, Reader<TEnv, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(project.Curry(x)));

    public Reader<TEnv, TResult> SelectMany<TElement, TResult>(Func<T, Reader<TEnv, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(bind, project);

    public T Run(TEnv env) => runner(env);

    public static implicit operator Reader<TEnv, T>(T value) => Return(value);

    public static Reader<TEnv, T> operator >>(Reader<TEnv, T> x, Reader<TEnv, T> y)
        => x.Then(y);

    public static Reader<TEnv, T> operator >>(Reader<TEnv, T> x, Reader<TEnv, Unit> y)
        => x.Then(y);
}
