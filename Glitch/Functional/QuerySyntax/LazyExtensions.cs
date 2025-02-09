namespace Glitch.Functional.QuerySyntax
{
    public static class LazyExtensions
    {
        public static Lazy<TResult> Select<T, TResult>(this Lazy<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Lazy<TResult> SelectMany<T, TResult>(this Lazy<T> source, Func<T, Lazy<TResult>> bind)
            => source.AndThen(bind);

        public static Lazy<TResult> SelectMany<T, TElement, TResult>(this Lazy<T> source, Func<T, Lazy<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));
    }
}
