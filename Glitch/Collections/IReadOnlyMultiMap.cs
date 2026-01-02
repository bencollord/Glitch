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
public interface IReadOnlyMultiMap<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    where TKey : notnull
{
    /// <summary>
    /// Gets the entire list of items for the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    IReadOnlyList<TValue> this[TKey key] { get; }

    /// <summary>
    /// Gets the item under <paramref name="key"/> at <paramref name="index"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    TValue this[TKey key, int index] { get; }

    /// <summary>
    /// Gets the count of individual keys in the map.
    /// </summary>
    int KeyCount { get; }

    /// <summary>
    /// Gets the full count of all entries in the map.
    /// </summary>
    int EntryCount { get; }

    /// <summary>
    /// Gets a list of all the keys in the map.
    /// </summary>
    IEnumerable<TKey> Keys { get; }

    /// <summary>
    /// Gets a list of the full, flattened list of values in the map.
    /// </summary>
    IEnumerable<TValue> Values { get; }

    /// <summary>
    /// Gets the count of items under <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    new int Count(TKey key);

    /// <summary>
    /// Returns true if the map contains any items under <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool ContainsKey(TKey key);

    /// <summary>
    /// Attempts to get all <paramref name="values">values</paramref> under <paramref name="key"/>
    /// and returns true if successful.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    bool TryGetValues(TKey key, [MaybeNullWhen(false)] out IReadOnlyList<TValue> values);

    /// <summary>
    /// Attempts to get the <paramref name="value"/> under <paramref name="key"/> at <paramref name="index"/>
    /// and returns true if successful.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryGetValue(TKey key, int index, [MaybeNullWhen(false)] out TValue? value);

    /// <summary>
    /// Gets a <see cref="ILookup{TKey, TElement}">lookup</see> of 
    /// groupings under the keys.
    /// </summary>
    /// <returns></returns>
    ILookup<TKey, TValue> ToLookup();
}