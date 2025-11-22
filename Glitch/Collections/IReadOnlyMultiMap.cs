using System.Diagnostics.CodeAnalysis;

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

        bool TryGetList(TKey key, out IEnumerable<TValue> list);

        bool TryGetValue(TKey key, int index, [NotNullWhen(true)] out TValue? value);
    }
}