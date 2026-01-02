using System.Collections;

namespace Glitch.Collections;

/// <summary>
/// Convenience class for a list which needs to enforce
/// no mutation, but still provides soem of the extra methods
/// on <see cref="List{T}"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ReadOnlyList<T> : IReadOnlyList<T>
{
    public static ReadOnlyList<T> Empty = new([]);

    private List<T> items;

    public ReadOnlyList(IEnumerable<T> items)
    {
        this.items = items as List<T> ?? items.ToList();
    }

    public T this[int index] => items[index];

    public int Count => items.Count;

    public int BinarySearch(T item) => items.BinarySearch(item);
    public int BinarySearch(T item, IComparer<T>? comparer) => items.BinarySearch(item, comparer);
    public int BinarySearch(int index, int count, T item, IComparer<T>? comparer) => items.BinarySearch(index, count, item, comparer);

    public bool Contains(T item) => items.Contains(item);

    public ReadOnlyList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) => new(items.ConvertAll(converter));

    public bool Exists(Predicate<T> match) => items.Exists(match);
    public T? Find(Predicate<T> match) => items.Find(match);
    public ReadOnlyList<T> FindAll(Predicate<T> match) => new(items.FindAll(match));
    public int FindIndex(Predicate<T> match) => items.FindIndex(match);
    public int FindIndex(int startIndex, Predicate<T> match) => items.FindIndex(startIndex, match);
    public int FindIndex(int startIndex, int count, Predicate<T> match) => items.FindIndex(startIndex, count, match);

    public T? FindLast(Predicate<T> match) => items.FindLast(match);
    public int FindLastIndex(Predicate<T> match) => items.FindLastIndex(match);
    public int FindLastIndex(int startIndex, Predicate<T> match) => items.FindLastIndex(startIndex, match);
    public int FindLastIndex(int startIndex, int count, Predicate<T> match) => items.FindLastIndex(startIndex, count, match);

    public void ForEach(Action<T> action) => items.ForEach(action);
    
    public ReadOnlyList<T> Slice(int start, int length) => new(items.Slice(start, length));

    public int IndexOf(T item) => items.IndexOf(item);
    public int IndexOf(T item, int startIndex) => items.IndexOf(item, startIndex);
    public int IndexOf(T item, int index, int count) => IndexOf(item, index, count);

    public bool TrueForAll(Predicate<T> match) => items.TrueForAll(match);
    public int LastIndexOf(T item) => items.LastIndexOf(item);
    public int LastIndexOf(T item, int startIndex) => items.LastIndexOf(item, startIndex);
    public int LastIndexOf(T item, int startIndex, int count) => items.LastIndexOf(item, startIndex, count);
    public T[] ToArray() => items.ToArray();

    public void CopyTo(T[] array) => items.CopyTo(array);
    public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);
    public void CopyTo(int index, T[] array, int arrayIndex, int count) => items.CopyTo(index, array, arrayIndex, count);

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
