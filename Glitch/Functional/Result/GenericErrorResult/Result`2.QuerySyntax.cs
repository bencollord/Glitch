namespace Glitch.Functional
{
    public static class Result2Extensions
    {
        public static Result<TResult, TError> Select<TOkay, TError, TResult>(this Result<TOkay, TError> source, Func<TOkay, TResult> mapper)
        => source.Map(mapper);

        public static Result<TResult, TError> SelectMany<TOkay, TError, TResult>(this Result<TOkay, TError> source, Func<TOkay, Result<TResult, TError>> bind)
            => source.AndThen(bind);

        public static Result<TResult, TError> SelectMany<TOkay, TError, TElement, TResult>(this Result<TOkay, TError> source, Func<TOkay, Result<TElement, TError>> bind, Func<TOkay, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));
    }
}