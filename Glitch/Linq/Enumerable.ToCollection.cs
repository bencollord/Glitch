using Glitch.Collections;

namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
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

    public static Deque<T> ToDeque<T>(this IEnumerable<T> source) => new Deque<T>(source);

    // TODO TO MultiMap
}
