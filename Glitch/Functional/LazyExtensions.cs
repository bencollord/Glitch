namespace Glitch.Functional
{
    public static class LazyExtensions
    {
        public static Lazy<TResult> Map<T, TResult>(this Lazy<T> lazy, Func<T, TResult> mapper)
            => new(() => mapper(lazy.Value));

        public static Lazy<TResult> Apply<T, TResult>(this Lazy<T> lazy, Lazy<Func<T, TResult>> function)
            => lazy.AndThen(v => function.Map(fn => fn(v)));

        public static Lazy<TResult> AndThen<T, TResult>(this Lazy<T> lazy, Func<T, Lazy<TResult>> mapper)
            => new(() => mapper(lazy.Value).Value);

        public static Lazy<TResult> AndThen<T, TElement, TResult>(this Lazy<T> lazy, Func<T, Lazy<TElement>> bind, Func<T, TElement, TResult> project)
            => lazy.AndThen(x => bind(x).Map(y => project(x, y)));

        public static Lazy<T> Do<T>(this Lazy<T> lazy, Action<T> action) 
            => new(() =>
            {
                var value = lazy.Value;
                action(value);
                return value;
            });
    }
}
