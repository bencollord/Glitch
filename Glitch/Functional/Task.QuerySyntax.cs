namespace Glitch.Functional
{
    public static partial class TaskExtensions
    {
        public static Task<TResult> Select<T, TResult>(this Task<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Task<TResult> SelectMany<T, TResult>(this Task<T> source, Func<T, Task<TResult>> bind)
            => source.AndThen(bind);

        public static Task<TResult> SelectMany<T, TElement, TResult>(this Task<T> source, Func<T, Task<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Task<T> Where<T>(this Task<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);
    }
}
