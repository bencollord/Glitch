namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<TResult> Cartesian<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> projection)
    {
        return source.CrossJoin(source, projection);
    }

    public static IEnumerable<TResult> CrossJoin<TSource, TOther, TResult>(this IEnumerable<TSource> source, IEnumerable<TOther> other, Func<TSource, TOther, TResult> projection)
    {
        return source.SelectMany(_ => other, projection);
    }
}
