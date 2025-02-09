namespace Glitch.Functional.QuerySyntax
{
    public static class TryExtensions
    {
        public static Try<TResult> Select<T, TResult>(this Try<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Try<TResult> SelectMany<T, TResult>(this Try<T> source, Func<T, Try<TResult>> bind)
            => source.AndThen(bind);

        public static Try<TResult> SelectMany<T, TElement, TResult>(this Try<T> source, Func<T, Try<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Try<T> Where<T>(this Try<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);
    }
}
