namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static IEffect<TInput, TResult> Select<TInput, T, TResult>(this IEffect<TInput, T> source, Func<T, TResult> map)
            => new MapEffect<TInput, T, TResult>(source, map);

        public static IEffect<TInput, TResult> SelectMany<TInput, T, TResult>(this IEffect<TInput, T> source, Func<T, IEffect<TInput, TResult>> bind)
            => source.AndThen(bind);

        public static IEffect<TInput, TResult> SelectMany<TInput, T, TElement, TResult>(this IEffect<TInput, T> source, Func<T, IEffect<TInput, TElement>> bind, Func<T, TElement, TResult> projection)
            => source.AndThen(v => bind(v).Select(x => projection(v, x)));

        private class MapEffect<TInput, T, TResult> : IEffect<TInput, TResult>
        {
            private readonly IEffect<TInput, T> source;
            private readonly Func<T, TResult> selector;

            internal MapEffect(IEffect<TInput, T> source, Func<T, TResult> selector)
            {
                this.source = source;
                this.selector = selector;
            }

            public TResult Run(TInput input)
            {
                return selector(source.Run(input));
            }
        }
    }
}