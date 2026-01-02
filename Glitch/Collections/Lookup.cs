using Glitch.Collections.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Glitch.Collections;

/// <summary>
/// Represents a read-only lookup of groupings by key.
/// </summary>
/// <remarks>
/// Essentially like <see cref="System.Linq.Lookup{TKey, TElement}"/>, but without
/// having its constructor locked down so nobody can use it like a regular collection.
/// </remarks>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class Lookup<TKey, TValue> : ILookup<TKey, TValue>
    where TKey : notnull
{
    private readonly Dictionary<TKey, IEnumerable<TValue>> dictionary;

    public Lookup(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        : this(entries, EqualityComparer<TKey>.Default) { }

    public Lookup(IEnumerable<KeyValuePair<TKey, TValue>> entries, IEqualityComparer<TKey> keyComparer)
    {
        dictionary = entries.GroupBy(e => e.Key, e => e.Value, keyComparer)
                            .ToDictionary(e => e.Key, e => e.AsEnumerable());
    }

    public Lookup(ILookup<TKey, TValue> lookup)
    {
        dictionary = lookup.ToDictionary(x => x.Key, x => x.AsEnumerable());
    }

    public Lookup(ILookup<TKey, TValue> lookup, IEqualityComparer<TKey> keyComparer)
    {
        dictionary = lookup.ToDictionary(x => x.Key, x => x.AsEnumerable(), keyComparer);
    }

    public Lookup(IDictionary<TKey, IEnumerable<TValue>> dictionary)
    {
        this.dictionary = dictionary.ToDictionary();
    }

    public Lookup(IDictionary<TKey, IEnumerable<TValue>> dictionary, IEqualityComparer<TKey> keyComparer)
    {
        this.dictionary = dictionary.ToDictionary(keyComparer);
    }

    public Lookup(IReadOnlyMultiMap<TKey, TValue> map)
    {
        var dictionary = new Dictionary<TKey, IEnumerable<TValue>>();

        foreach (var key in map.Keys)
        {
            dictionary.Add(key, map[key]);
        }

        this.dictionary = dictionary;
    }

    public IEnumerable<TValue> this[TKey key] => dictionary[key];

    public int Count => dictionary.Count;

    public IEqualityComparer<TKey> Comparer => dictionary.Comparer;

    public bool Contains(TKey key) => dictionary.ContainsKey(key);

    public Enumerator GetEnumerator() => new(this);

    IEnumerator<IGrouping<TKey, TValue>> IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<IGrouping<TKey, TValue>>
    {
        private readonly Lookup<TKey, TValue> lookup;
        private Dictionary<TKey, IEnumerable<TValue>>.Enumerator? dictionaryEnumerator;

        internal Enumerator(Lookup<TKey, TValue> multiMap)
        {
            this.lookup = multiMap;
        }

        public Grouping<TKey, TValue> Current { get; private set; }

        readonly IGrouping<TKey, TValue> IEnumerator<IGrouping<TKey, TValue>>.Current => Current;
        readonly object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (!dictionaryEnumerator.HasValue)
            {
                dictionaryEnumerator = lookup.dictionary.GetEnumerator();
            }

            if (dictionaryEnumerator.Value.MoveNext())
            {
                Current = new(dictionaryEnumerator.Value.Current.Key, dictionaryEnumerator.Value.Current.Value);
                return true;
            }

            return false;
        }

        public readonly void Dispose()
        {
            dictionaryEnumerator?.Dispose();
        }

        public void Reset()
        {
            dictionaryEnumerator = null;
        }
    }
}
