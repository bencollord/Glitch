namespace Glitch.Functional
{
    public static class LazyExtensions
    {
        public static Lazy<TResult> Map<T, TResult>(this Lazy<T> lazy, Func<T, TResult> mapper)
            => new(() => mapper(lazy.Value));

        public static Lazy<TResult> Apply<T, TResult>(this Lazy<T> lazy, Lazy<Func<T, TResult>> function)
            => lazy.FlatMap(v => function.Map(fn => fn(v)));

        public static Lazy<TResult> FlatMap<T, TResult>(this Lazy<T> lazy, Func<T, Lazy<TResult>> mapper)
            => new(() => mapper(lazy.Value).Value);

        public static Lazy<T> Do<T>(this Lazy<T> lazy, Action<T> action) 
            => new(() =>
            {
                var value = lazy.Value;
                action(value);
                return value;
            });
    }
}
