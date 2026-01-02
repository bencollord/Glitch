using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Collections
{
    public interface IImmutableMultiMap<TKey, TValue> : IReadOnlyMultiMap<TKey, TValue>
        where TKey : notnull
    {
        /// <inheritdoc cref="IReadOnlyMultiMap{TKey, TValue}.this[TKey key]"/>
        new IImmutableList<TValue> this[TKey key] { get; }

        IImmutableMultiMap<TKey, TValue> Add(TKey key, TValue value);
        IImmutableMultiMap<TKey, TValue> AddRange(TKey key, IEnumerable<TValue> values);
        IImmutableMultiMap<TKey, TValue> Clear();

        IImmutableMultiMap<TKey, TValue> Remove(TKey key, TValue value);
        IImmutableMultiMap<TKey, TValue> RemoveAt(TKey key, int index);
        IImmutableMultiMap<TKey, TValue> RemoveAll(TKey key);
        IImmutableMultiMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);
        IImmutableMultiMap<TKey, TValue> SetItem(TKey key, int index, TValue item);
        IImmutableMultiMap<TKey, TValue> SetList(TKey key, IImmutableList<TValue> list);

        bool TryGetKey(TKey key, out TKey actualKey);
        bool TryGetList(TKey key, [NotNullWhen(true)] out IImmutableList<TValue>? list);
    }
}