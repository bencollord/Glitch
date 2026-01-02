using Glitch.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Collections;

public static class ImmutableMultiMap
{
    public static ImmutableMultiMap<TKey, TValue> Create<TKey, TValue>(IReadOnlyMultiMap<TKey, TValue> multiMap)
        where TKey : notnull
    {
        var dict = multiMap.Keys.Select(k => KeyValuePair.Create(k, multiMap[k].ToImmutableList() as IImmutableList<TValue>))
                                .ToImmutableDictionary();

        return new ImmutableMultiMap<TKey, TValue>(dict);
    }

    public static ImmutableMultiMap<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(IEqualityComparer<TKey>? keyComparer) 
        where TKey : notnull
    {
        return new(keyComparer ?? EqualityComparer<TKey>.Default);
    }

    public static ImmutableMultiMap<TKey, TValue> Create<TKey, TValue>(IEnumerable<IGrouping<TKey, TValue>> groupings)
        where TKey : notnull
        => new(ImmutableDictionary.CreateRange(groupings.Select(l => CreatePair(l.Key, l))));

    public static ImmutableMultiMap<TKey, TValue> Create<TKey, TValue>(IEnumerable<IGrouping<TKey, TValue>> groupings, IEqualityComparer<TKey>? keyComparer)
        where TKey : notnull
        => new(ImmutableDictionary.CreateRange(keyComparer, groupings.Select(l => CreatePair(l.Key, l))));

    public static ImmutableMultiMap<TKey, TValue> ToImmutableMultiMap<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IEqualityComparer<TKey>? keyComparer) 
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(keySelector, nameof(keySelector));
        ArgumentNullException.ThrowIfNull(elementSelector, nameof(elementSelector));

        var lookup = source.ToLookup(keySelector, elementSelector, keyComparer);

        return Create(lookup);
    }

    public static ImmutableMultiMap<TKey, TSource> ToImmutableMultiMap<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        where TKey : notnull
    {
        return ToImmutableMultiMap(source, keySelector, v => v, null);
    }

    public static ImmutableMultiMap<TKey, TSource> ToImmutableMultiMap<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer) 
        where TKey : notnull
    {
        return ToImmutableMultiMap(source, keySelector, v => v, keyComparer);
    }

    public static ImmutableMultiMap<TKey, TValue> ToImmutableMultiMap<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector) 
        where TKey : notnull
    {
        return ToImmutableMultiMap(source, keySelector, elementSelector, null);
    }

    public static ImmutableMultiMap<TKey, TValue> ToImmutableMultiMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? keyComparer) 
        where TKey : notnull
    {
        if (source is ImmutableMultiMap<TKey, TValue> existingMultiMap)
        {
            return existingMultiMap.WithComparer(keyComparer);
        }

        return Create(source.GroupBy(s => s.Key, s => s.Value, keyComparer), keyComparer);
    }

    public static ImmutableMultiMap<TKey, TValue> ToImmutableMultiMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) 
        where TKey : notnull
    {
        return ToImmutableMultiMap(source, null);
    }

    private static KeyValuePair<TKey, IImmutableList<TValue>> CreatePair<TKey, TValue>(TKey key, IEnumerable<TValue> values)
        => new(key, values.ToImmutableList());
}

public partial class ImmutableMultiMap<TKey, TValue> : IReadOnlyMultiMap<TKey, TValue>
    where TKey : notnull
{
    public static readonly ImmutableMultiMap<TKey, TValue> Empty = new(ImmutableDictionary<TKey, IImmutableList<TValue>>.Empty);

    private readonly ImmutableDictionary<TKey, IImmutableList<TValue>> dictionary;

    internal ImmutableMultiMap(ImmutableDictionary<TKey, IImmutableList<TValue>> dictionary)
    {
        this.dictionary = dictionary;
    }

    public TValue this[TKey key, int index] => dictionary[key][index];

    public IImmutableList<TValue> this[TKey key] => dictionary[key];

    public IEqualityComparer<TKey> Comparer => dictionary.KeyComparer;

    public IEnumerable<TKey> Keys => dictionary.Keys;

    public IEnumerable<TValue> Values => dictionary.Values.Flatten();

    public int KeyCount => dictionary.Count;

    public int EntryCount => dictionary.Values.Sum(e => e.Count);

    public ImmutableMultiMap<TKey, TValue> WithComparer(IEqualityComparer<TKey>? comparer)
        => new(dictionary.WithComparers(comparer));

    public ImmutableMultiMap<TKey, TValue> Add(TKey key, TValue value)
    {
        var dict = TryGetList(key, out var existing)
                 ? dictionary.SetItem(key, existing.Add(value))
                 : dictionary.SetItem(key, [value]);

        return new(dict);
    }

    public ImmutableMultiMap<TKey, TValue> Add(TKey key, params TValue[] values) => AddRange(key, values);

    public ImmutableMultiMap<TKey, TValue> AddRange(TKey key, IEnumerable<TValue> values) => AddRange(key, ImmutableList.CreateRange(values));

    public ImmutableMultiMap<TKey, TValue> AddRange(TKey key, IImmutableList<TValue> list)
    {
        var dict = TryGetList(key, out var existing)
                 ? dictionary.SetItem(key, existing.AddRange(list))
                 : dictionary.SetItem(key, list);

        return new(dict);
    }

    public ImmutableMultiMap<TKey, TValue> Clear() => new(dictionary.Clear());

    public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return dictionary
            .SelectMany(pair => pair.Value, (p, v) => KeyValuePair.Create(p.Key, v))
            .GetEnumerator();
    }

    public Builder ToBuilder() => new(this);

    public ImmutableMultiMap<TKey, TValue> Remove(TKey key) => new(dictionary.Remove(key));
    
    public ImmutableMultiMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys) => new(dictionary.RemoveRange(keys));
    
    public ImmutableMultiMap<TKey, TValue> SetItem(TKey key, int index, TValue item) => new(dictionary.SetItem(key, dictionary[key].SetItem(index, item)));
    
    public ImmutableMultiMap<TKey, TValue> SetList(TKey key, IImmutableList<TValue> list) => new(dictionary.SetItem(key, list));

    public bool TryGetKey(TKey key, out TKey actualKey) => dictionary.TryGetKey(key, out actualKey);

    public bool TryGetValue(TKey key, int index, [NotNullWhen(true)] out TValue? value)
    {
        if(TryGetList(key, out var list) && list.Count > index)
        {
            value = list[index];
            return value != null;
        }

        value = default;
        return false;
    }

    public int Count(TKey key) => TryGetList(key, out var list) ? list.Count : 0;

    public bool TryGetList(TKey key, [NotNullWhen(true)] out IImmutableList<TValue>? list) => dictionary.TryGetValue(key, out list);
    
    public bool TryGetValues(TKey key, [MaybeNullWhen(false)] out IReadOnlyList<TValue> values)
    {
        bool success = TryGetList(key, out var list);
        values = list;
        return success;
    }

    public ILookup<TKey, TValue> ToLookup() => throw new NotImplementedException();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // =========================
}
