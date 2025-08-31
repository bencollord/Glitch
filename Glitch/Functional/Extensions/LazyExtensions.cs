using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class LazyExtensions
    {
        public static Lazy<TResult> Select<T, TResult>(this Lazy<T> lazy, Func<T, TResult> mapper)
            => new(() => mapper(lazy.Value));

        public static Lazy<TResult> Apply<T, TResult>(this Lazy<T> lazy, Lazy<Func<T, TResult>> function)
            => lazy.AndThen(v => function.Select(fn => fn(v)));

        public static Lazy<TResult> AndThen<T, TResult>(this Lazy<T> lazy, Func<T, Lazy<TResult>> mapper)
            => new(() => mapper(lazy.Value).Value);

        public static Lazy<TResult> AndThen<T, TElement, TResult>(this Lazy<T> lazy, Func<T, Lazy<TElement>> bind, Func<T, TElement, TResult> project)
            => lazy.AndThen(x => bind(x).Select(y => project(x, y)));

        public static Lazy<TResult> SelectMany<T, TResult>(this Lazy<T> source, Func<T, Lazy<TResult>> bind)
            => source.AndThen(bind);

        public static Lazy<TResult> SelectMany<T, TElement, TResult>(this Lazy<T> source, Func<T, Lazy<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

        public static Lazy<Option<T>> Where<T>(this Lazy<T> source, Func<T, bool> predicate)
            => source.Select(s => predicate(s) ? Option.Some(s) : Option.None);

        public static Lazy<T> Do<T>(this Lazy<T> lazy, Action<T> action) 
            => new(() =>
            {
                var value = lazy.Value;
                action(value);
                return value;
            });
    }
}
