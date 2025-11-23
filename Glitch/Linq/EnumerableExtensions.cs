using System.Collections;

namespace Glitch.Linq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> LeftJoin<TLeft, TRight, TKey, TResult>(
                this IEnumerable<TLeft> left,
                IEnumerable<TRight> right,
                Func<TLeft, TKey> leftKeySelector,
                Func<TRight, TKey> rightKeySelector,
                Func<TLeft, TRight?, TResult> resultSelector
            ) => left.LeftJoin(
                     right,
                     leftKeySelector,
                     rightKeySelector,
                     resultSelector,
                     EqualityComparer<TKey>.Default
                );

        public static IEnumerable<TResult> LeftJoin<TLeft, TRight, TKey, TResult>(
                this IEnumerable<TLeft> left,
                IEnumerable<TRight> right,
                Func<TLeft, TKey> leftKeySelector,
                Func<TRight, TKey> rightKeySelector,
                Func<TLeft, TRight?, TResult> resultSelector,
                IEqualityComparer<TKey> keyComparer
            )
        {
            ArgumentNullException.ThrowIfNull(left, nameof(left));
            ArgumentNullException.ThrowIfNull(right, nameof(right));
            ArgumentNullException.ThrowIfNull(leftKeySelector, nameof(leftKeySelector));
            ArgumentNullException.ThrowIfNull(rightKeySelector, nameof(rightKeySelector));
            ArgumentNullException.ThrowIfNull(resultSelector, nameof(resultSelector));
            ArgumentNullException.ThrowIfNull(keyComparer, nameof(keyComparer));

            return left.GroupJoin(
                right,
                leftKeySelector,
                rightKeySelector,
                (l, r) => new { left = l, right = r },
                keyComparer
            ).SelectMany(
                x => x.right.DefaultIfEmpty(),
                (x, right) => resultSelector(x.left, right)
            );
        }

        public static IEnumerable<TResult> RightJoin<TLeft, TRight, TKey, TResult>(
                this IEnumerable<TLeft> left,
                IEnumerable<TRight> right,
                Func<TLeft, TKey> leftKeySelector,
                Func<TRight, TKey> rightKeySelector,
                Func<TLeft?, TRight, TResult> resultSelector
            ) => left.RightJoin(
                     right,
                     leftKeySelector,
                     rightKeySelector,
                     resultSelector,
                     EqualityComparer<TKey>.Default
                );

        public static IEnumerable<TResult> RightJoin<TLeft, TRight, TKey, TResult>(
                this IEnumerable<TLeft> left,
                IEnumerable<TRight> right,
                Func<TLeft, TKey> leftKeySelector,
                Func<TRight, TKey> rightKeySelector,
                Func<TLeft?, TRight, TResult> resultSelector,
                IEqualityComparer<TKey> keyComparer
            ) => right.LeftJoin(
                     left,
                     rightKeySelector,
                     leftKeySelector,
                     (r, l) => resultSelector(l, r),
                     keyComparer
                 );

        public static IEnumerable<TResult> FullJoin<TLeft, TRight, TKey, TResult>(
                this IEnumerable<TLeft> left,
                IEnumerable<TRight> right,
                Func<TLeft, TKey> leftKeySelector,
                Func<TRight, TKey> rightKeySelector,
                Func<TLeft?, TRight?, TResult> resultSelector
            ) => left.FullJoin(
                     right,
                     leftKeySelector,
                     rightKeySelector,
                     resultSelector,
                     EqualityComparer<TKey>.Default
                );

        public static IEnumerable<TResult> FullJoin<TLeft, TRight, TKey, TResult>(
                this IEnumerable<TLeft> left,
                IEnumerable<TRight> right,
                Func<TLeft, TKey> leftKeySelector,
                Func<TRight, TKey> rightKeySelector,
                Func<TLeft?, TRight?, TResult> resultSelector,
                IEqualityComparer<TKey> keyComparer
            )
        {
            return left.LeftJoin(
                     right,
                     leftKeySelector,
                     rightKeySelector,
                     resultSelector,
                     keyComparer
                 ).Union(
                     left.RightJoin(
                        right,
                        leftKeySelector,
                        rightKeySelector,
                        resultSelector,
                        keyComparer
                     )
                 );
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
            => source.SelectMany(x => x);

        public static TCollection Collect<T, TCollection>(this IEnumerable<T> source, TCollection collection)
            where TCollection : ICollection<T>
        {
            foreach (var item in source)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> source) => new Stack<T>(source);

        public static Queue<T> ToQueue<T>(this IEnumerable<T> source) => new Queue<T>(source);

        public static T Random<T>(this IEnumerable<T> source) => source.Shuffle().First();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
            => items.OrderBy(_ => Guid.NewGuid());

        public static IEnumerable<TResult> Cartesian<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> projection)
        {
            return source.CrossJoin(source, projection);
        }

        public static IEnumerable<TResult> CrossJoin<TSource, TOther, TResult>(this IEnumerable<TSource> source, IEnumerable<TOther> other, Func<TSource, TOther, TResult> projection)
        {
            return source.SelectMany(_ => other, projection);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] others) => source.Except(others.AsEnumerable());

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer, params T[] others) 
            => source.Except(others, comparer);

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T other, IEqualityComparer<T> comparer) 
            => source.Except([other], comparer);

        public static IEnumerable<T> ExceptWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.Where(e => !predicate(e));

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }
    }
}
