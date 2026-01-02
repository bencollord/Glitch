using Glitch.Linq;
using System.Collections.Immutable;

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