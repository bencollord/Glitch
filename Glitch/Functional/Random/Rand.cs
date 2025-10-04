namespace Glitch.Functional
{
    public class Rand<T>
    {
        private Func<Random> factory;
        private Func<Random, T> generate;

        internal Rand(Func<Random, T> generate) : this(CreateDefault, generate) { }

        internal Rand(Func<Random> factory, Func<Random, T> generate)
        {
            this.factory = factory;
            this.generate = generate;
        }

        public Rand<T> Seed(int seed) => new(() => new Random(seed), generate);

        public Rand<IEnumerable<T>> Repeat(int count)
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

        public override string ToString() => $"Random {typeof(T).Name}";

        // Monad Implementation
        public static Rand<T> Return(T value) => new(CreateDefault, _ => value);

        public Rand<TResult> Select<TResult>(Func<T, TResult> map) => new(factory, generate.Then(map));

        public Rand<TResult> AndThen<TResult>(Func<T, Rand<TResult>> bind)
        {
            return new(factory, rng => 
            {
                var x = generate(rng);
                var y = bind(x);

                return y.generate(rng);
            });
        }

        // Monad boilerplate
        public Rand<TResult> AndThen<TElement, TResult>(Func<T, Rand<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));

        public Rand<TResult> Apply<TResult>(Rand<Func<T, TResult>> apply)
            => apply.AndThen(fn => Select(val => fn(val)));

        public Rand<TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        public Rand<TResult> SelectMany<TElement, TResult>(Func<T, Rand<TElement>> bind, Func<T, TElement, TResult> project)
           => AndThen(bind, project);

        private static Random CreateDefault() => new();

#pragma warning disable IDE0051 // For LinqPad
        object? ToDump() => Run();
#pragma warning restore IDE0051 // For LinqPad
    }
}
