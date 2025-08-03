namespace Glitch.Functional
{
    public static class ResultQuerySyntax
    {
        public static Result<TResult> Select<T, TResult>(this Result<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Result<TResult> SelectMany<T, TResult>(this Result<T> source, Func<T, Result<TResult>> bind)
            => source.AndThen(bind);

        public static Result<TResult> SelectMany<T, TElement, TResult>(this Result<T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Result<T> Where<T>(this Result<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);
    }
}
