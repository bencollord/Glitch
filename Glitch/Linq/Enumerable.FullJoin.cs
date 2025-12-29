namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
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
