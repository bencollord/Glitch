namespace Glitch.Functional
{
    public static partial class EffectExtensions
    {
        public static Effect<TResult> Select<T, TResult>(this Effect<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Effect<TResult> SelectMany<T, TResult>(this Effect<T> source, Func<T, Effect<TResult>> bind)
            => source.AndThen(bind);

        public static Effect<TResult> SelectMany<T, TElement, TResult>(this Effect<T> source, Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Effect<TResult> SelectMany<T, TResult>(this Effect<T> source, Func<T, Result<TResult>> bind)
            => source.AndThen(bind);

        public static Effect<TResult> SelectMany<T, TElement, TResult>(this Effect<T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Effect<T> Where<T>(this Effect<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);
    }
}
