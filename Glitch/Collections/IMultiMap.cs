using System.Diagnostics.CodeAnalysis;

namespace Glitch.Collections;

/// <summary>
/// A dictionary that allows adding multiple entries under one key.
/// </summary>
/// <remarks>
/// The enumerator of this collection treats it as a flat list of
/// <see cref="KeyValuePair{TKey, TValue}"/>s that allows duplicate keys.
/// </remarks>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IMultiMap<TKey, TValue> : IReadOnlyMultiMap<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>
    where TKey : notnull
{
    /// <summary>
    /// Gets or sets the entire list of items for the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    new IList<TValue> this[TKey key] { get; set; }

    /// <summary>
    /// Gets or sets the item under <paramref name="key"/> at <paramref name="index"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    new TValue this[TKey key, int index] { get; set; }

    /// <summary>
    /// Adds a new entry under <paramref name="key"/> for <paramref name="value"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void Add(TKey key, TValue value);

    /// <summary>
    /// Adds one or more entries under <paramref name="key"/> for <paramref name="values"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    void Add(TKey key, params TValue[] values);

    /// <summary>
    /// Adds a range of <paramref name="items"/> under <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="items"></param>
    void AddRange(TKey key, IEnumerable<TValue> items);

    /// <summary>
    /// Removes the entry for <paramref name="value"/> under <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>
    /// Returns <see langword="true"/> if the value existed under the
    /// key and was successfully removed.
    /// </returns>
    bool Remove(TKey key, TValue value);

    /// <summary>
    /// Removes the entry under <paramref name="key"/> at <paramref name="index"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="index"></param>
    /// <returns>
    /// Returns <see langword="true"/> if the item under the
    /// key and index existed and was successfully removed.
    /// </returns>
    bool RemoveAt(TKey key, int index);

    /// <summary>
    /// Removes all entries under <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>
    /// Returns the count of items that were removed, 
    /// or -1 if the key didn't exist.
    /// </returns>
    int RemoveAll(TKey key);

    /// <summary>
    /// Gets the mutable list of values under <paramref name="key"/>.
    /// If the key doesn't exist, creates an empty list for that key and returns it.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    IList<TValue> GetOrAddList(TKey key);

    /// <summary>
    /// Attempts to get the mutable <paramref name="list"/> under <paramref name="key"/>
    /// and returns true if successful.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    bool TryGetList(TKey key, [MaybeNullWhen(false)] out IList<TValue> list);
}