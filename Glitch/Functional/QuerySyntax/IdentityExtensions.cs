namespace Glitch.Functional.QuerySyntax
{
    public static class IdentityExtensions
    {
        public static Identity<TResult> Select<T, TResult>(this Identity<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Identity<TResult> SelectMany<T, TResult>(this Identity<T> source, Func<T, Identity<TResult>> bind)
            => source.AndThen(bind);

        public static Identity<TResult> SelectMany<T, TElement, TResult>(this Identity<T> source, Func<T, Identity<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        // This works!!
        public static Option<T> Where<T>(this Identity<T> source, Func<T, bool> predicate)
            => predicate(source.Value) ? Some(source.Value) : None;
    }
}