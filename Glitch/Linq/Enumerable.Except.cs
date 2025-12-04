namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] others) => source.Except(others.AsEnumerable());

    public static IEnumerable<T> Except<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer, params T[] others) 
        => source.Except(others, comparer);

    public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T other, IEqualityComparer<T> comparer) 
        => source.Except([other], comparer);

    public static IEnumerable<T> ExceptWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source.Where(e => !predicate(e));
}
