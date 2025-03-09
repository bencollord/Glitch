namespace Glitch.Functional.QuerySyntax
{
    public static class TryExtensions
    {
        public static Fallible<TResult> Select<T, TResult>(this Fallible<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Fallible<TResult> SelectMany<T, TResult>(this Fallible<T> source, Func<T, Fallible<TResult>> bind)
            => source.AndThen(bind);

        public static Fallible<TResult> SelectMany<T, TElement, TResult>(this Fallible<T> source, Func<T, Fallible<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Fallible<TResult> SelectMany<T, TResult>(this Fallible<T> source, Func<T, Result<TResult>> bind)
            => source.AndThen(bind);

        public static Fallible<TResult> SelectMany<T, TElement, TResult>(this Fallible<T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Fallible<T> Where<T>(this Fallible<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);
    }
}
