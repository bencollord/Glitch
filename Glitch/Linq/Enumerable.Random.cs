namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
    public static T Random<T>(this IEnumerable<T> source) => source.Shuffle().First();

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        => items.OrderBy(_ => Guid.NewGuid());
}
