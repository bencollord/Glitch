using Glitch.Functional;
using Glitch.Functional.Results;

namespace Glitch.Collections
{
    public interface IReadOnlyMultiMap<TKey, TValue> 
        : IEnumerable<KeyValuePair<TKey, TValue>>,
          IReadOnlyDictionary<TKey, IEnumerable<TValue>>
            where TKey : notnull
    {
        TValue this[TKey key, int index] { get; }

        int KeyCount { get; }

        int ValueCount { get; }

        new IEnumerable<TValue> Values { get; }

        Option<IEnumerable<TValue>> TryGetList(TKey key)
            => TryGetList(key, out var list) ? Some(list) : None;

        bool TryGetList(TKey key, out IEnumerable<TValue> list);

        Option<TValue> TryGetValue(TKey key, int index)
            => TryGetValue(key, index, out var value) ? Some(value) : None;

        bool TryGetValue(TKey key, int index, out TValue value);
    }
}