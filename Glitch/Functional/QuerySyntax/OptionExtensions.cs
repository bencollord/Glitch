namespace Glitch.Functional.QuerySyntax
{
    public static class OptionExtensions
    {
        public static Option<TResult> Select<T, TResult>(this Option<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Option<TResult> SelectMany<T, TResult>(this Option<T> source, Func<T, Option<TResult>> bind)
            => source.AndThen(bind);

        public static Option<TResult> SelectMany<T, TElement, TResult>(this Option<T> source, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Option<T> Where<T>(this Option<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);
    }
}