namespace Glitch.Linq;

public static partial class EnumerableExtensions
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
}
