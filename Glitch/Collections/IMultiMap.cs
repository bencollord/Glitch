namespace Glitch.Collections;

public interface IMultiMap<TKey, TValue> 
    : IEnumerable<KeyValuePair<TKey, TValue>>,
      IDictionary<TKey, IList<TValue>>
        where TKey : notnull
{
    TValue this[TKey key, int index] { get; set; }

    int KeyCount { get; }

    int ValueCount { get; }

    new IEnumerable<TValue> Values { get; }

    IList<TValue> Add(TKey key, TValue value);
    IList<TValue> Add(TKey key, params TValue[] values);
    IList<TValue> AddRange(TKey key, IEnumerable<TValue> items);
    IList<TValue> AddRange(TKey key, IList<TValue> list);

    bool TryGetList(TKey key, out IList<TValue> list) => TryGetValue(key, out list!);
    bool TryGetValue(TKey key, int index, out TValue? value);
}