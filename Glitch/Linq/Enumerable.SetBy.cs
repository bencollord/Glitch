namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
    public static IEnumerable<T> ExceptBy<T, TKey>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, TKey> keySelector) => source.ExceptBy(other.Select(keySelector), keySelector);
    public static IEnumerable<T> IntersectBy<T, TKey>(this IEnumerable<T> source, IEnumerable<T> other, Func<T, TKey> keySelector) => source.IntersectBy(other.Select(keySelector), keySelector);
}
