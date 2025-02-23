namespace Glitch.Functional.QuerySyntax
{
    // TODO Will this even work with the extra generic parameter?
    public static class ReaderExtensions
    {
        public static Reader<TEnv, TResult> Select<TEnv, T, TResult>(this Reader<TEnv, T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Reader<TEnv, TResult> SelectMany<TEnv, T, TResult>(this Reader<TEnv, T> source, Func<T, Reader<TEnv, TResult>> bind)
            => source.AndThen(bind);

        public static Reader<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Reader<TEnv, T> source, Func<T, Reader<TEnv, TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));
    }
}