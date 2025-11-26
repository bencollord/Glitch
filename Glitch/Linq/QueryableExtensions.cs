using System.Linq.Expressions;

namespace Glitch.Linq;

public static class QueryableExtensions
{
    public static IQueryable<TResult> LeftJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> left,
            IQueryable<TRight> right,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector
        )
    {
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(right, nameof(right));
        ArgumentNullException.ThrowIfNull(leftKeySelector, nameof(leftKeySelector));
        ArgumentNullException.ThrowIfNull(rightKeySelector, nameof(rightKeySelector));
        ArgumentNullException.ThrowIfNull(resultSelector, nameof(resultSelector));

        // Idiomatic left join to get left and right pair
        var query = left
            .GroupJoin(
                right,
                leftKeySelector,
                rightKeySelector,
                (l, r) => new { Left = l, Rights = r }
            ).SelectMany(
                x => x.Rights.DefaultIfEmpty(),
                (x, right) => new { x.Left, Right = right }
            );

        var pairType = query.GetType()
            .GetInterfaces()
            .Single(i => i.Name == "IQueryable`1")
            .GetGenericArguments()
            .Single();

        // Change result selector into a single argument function
        // that uses pair.Left and pair.Right
        var leftParameter  = resultSelector.Parameters[0];
        var rightParameter = resultSelector.Parameters[1];
        var pairParameter  = Expression.Parameter(pairType);

        var replacer = new ExpressionReplacer()
            .Replace(leftParameter, Expression.Property(pairParameter, "Left"))
            .Replace(rightParameter, Expression.Property(pairParameter, "Right"));

        var selectorType = typeof(Func<,>).MakeGenericType(pairType, typeof(TResult));
        var selectorBody = replacer.Visit(resultSelector.Body);
        var selector     = Expression.Lambda(selectorType, selectorBody, pairParameter);
        var selectMethod = QueryableMethods.Select.MakeGenericMethod(pairType, typeof(TResult));
        var selectCall   = Expression.Call(null, selectMethod, query.Expression, selector);

        return query.Provider.CreateQuery<TResult>(selectCall);
    }

    public static IQueryable<TResult> RightJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> left,
            IQueryable<TRight> right,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector
        )
    {
        var flippedResultSelector = Expression.Lambda<Func<TRight, TLeft, TResult>>(
            resultSelector.Body,
            resultSelector.Parameters.Reverse());

        return right.LeftJoin(
                 left,
                 rightKeySelector,
                 leftKeySelector,
                 flippedResultSelector
             );
    }

    public static IQueryable<TResult> FullJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> left,
            IQueryable<TRight> right,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector
        )
    {
        return left.LeftJoin(
                 right,
                 leftKeySelector,
                 rightKeySelector,
                 resultSelector
             ).Union(
                 left.RightJoin(
                    right,
                    leftKeySelector,
                    rightKeySelector,
                    resultSelector
                 )
             );
    }

    public static IQueryable<T> Flatten<T>(this IQueryable<IQueryable<T>> source)
        => source.SelectMany(x => x);

    public static T Random<T>(this IQueryable<T> source) => source.Shuffle().First();

    public static IQueryable<T> Shuffle<T>(this IQueryable<T> items)
        => items.OrderBy(_ => Guid.NewGuid());

    public static IQueryable<TResult> Cartesian<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TSource, TResult>> projection)
    {
        return source.SelectMany(_ => source, projection);
    }

    public static IQueryable<T> ExceptWhere<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
    {
        var negatedPredicate = Expression.Lambda<Func<T, bool>>(
            Expression.Negate(predicate.Body),
            predicate.Parameters);

        return source.Where(negatedPredicate);
    }
}
