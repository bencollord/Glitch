using System.Numerics;

namespace Glitch.Functional.Collections;

public static partial class Sequence
{
    public static Sequence<T> AsSequence<T>(this IEnumerable<T> items) => new(items);

    public static Sequence<T> Empty<T>() => Sequence<T>.Empty;

    public static Sequence<T> Single<T>(T item) => new([item]);

    public static Sequence<T> From<T>(IEnumerable<T> items) => new(items);

    public static Sequence<T> Of<T>(params T[] items) => new(items);

    public static Sequence<T> Repeat<T>(T item, int count)
        => new(Enumerable.Repeat(item, count));

    public static Sequence<T> Repeat<T>(Func<T> func, int count)
        => new(Enumerable.Repeat(func, count).Select(fn => fn()));

    public static Sequence<T> Range<T>(T start, T end)
        where T : IComparisonOperators<T, T, bool>, IIncrementOperators<T>
    {
        IEnumerable<T> Iter()
        {
            for (var i = start; i < end; i++)
            {
                yield return i;
            }
        }

        return new(Iter());
    }
}
