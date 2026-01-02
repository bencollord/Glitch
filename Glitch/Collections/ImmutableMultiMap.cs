using Glitch.Linq;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Collections;

public partial class ImmutableMultiMap<TKey, TValue> : IImmutableMultiMap<TKey, TValue> 
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

    public Builder ToBuilder() => new(this);

    public ImmutableMultiMap<TKey, TValue> Remove(TKey key, TValue value) => new(dictionary.SetItem(key, dictionary[key].Remove(value)));
    public ImmutableMultiMap<TKey, TValue> RemoveAt(TKey key, int index) => new(dictionary.SetItem(key, dictionary[key].RemoveAt(index)));
    public ImmutableMultiMap<TKey, TValue> RemoveAll(TKey key) => new(dictionary.Remove(key));

    public ImmutableMultiMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys) => new(dictionary.RemoveRange(keys));

    public ImmutableMultiMap<TKey, TValue> SetItem(TKey key, int index, TValue item) => new(dictionary.SetItem(key, dictionary[key].SetItem(index, item)));

    public ImmutableMultiMap<TKey, TValue> SetList(TKey key, IImmutableList<TValue> list) => new(dictionary.SetItem(key, list));

    public bool TryGetKey(TKey key, out TKey actualKey) => dictionary.TryGetKey(key, out actualKey);

    public bool TryGetValue(TKey key, int index, [NotNullWhen(true)] out TValue? value)
    {
        if (TryGetList(key, out var list) && list.Count > index)
        {
            value = list[index];
            return value != null;
        }

        value = default;
        return false;
    }

    public int Count(TKey key) => TryGetList(key, out var list) ? list.Count : 0;

    public bool TryGetList(TKey key, [NotNullWhen(true)] out IImmutableList<TValue>? list) => dictionary.TryGetValue(key, out list);

    public Enumerator GetEnumerator() => new(this);

    public ILookup<TKey, TValue> ToLookup() => new Lookup<TKey, TValue>(this);

    int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => EntryCount;
    IReadOnlyList<TValue> IReadOnlyMultiMap<TKey, TValue>.this[TKey key] => this[key];
    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.AddRange(TKey key, IEnumerable<TValue> values) => AddRange(key, values);
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.Clear() => Clear();
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys) => RemoveRange(keys);
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.SetItem(TKey key, int index, TValue item) => SetItem(key, index, item);
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.SetList(TKey key, IImmutableList<TValue> list) => SetList(key, list);
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.Remove(TKey key, TValue value) => Remove(key, value);
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.RemoveAt(TKey key, int index) => RemoveAt(key, index);
    IImmutableMultiMap<TKey, TValue> IImmutableMultiMap<TKey, TValue>.RemoveAll(TKey key) => RemoveAll(key);
    bool IReadOnlyMultiMap<TKey, TValue>.TryGetList(TKey key, [MaybeNullWhen(false)] out IReadOnlyList<TValue> values)
    {
        bool success = TryGetList(key, out var list);
        values = list;
        return success;
    }

    // TODO Eliminate duplication with MultiMap.Enumerator. Somehow. Considering C#'s type system REALLY doesn't want to honor inheritance with generic arguments.
    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private readonly ImmutableMultiMap<TKey, TValue> multiMap;
        private ImmutableDictionary<TKey, IImmutableList<TValue>>.Enumerator dictionaryEnumerator;
        private IEnumerator<TValue>? listEnumerator; // TODO Struct enumerator
        private State state = State.NotStarted;

        internal Enumerator(ImmutableMultiMap<TKey, TValue> multiMap)
        {
            this.multiMap = multiMap;
        }

        public KeyValuePair<TKey, TValue> Current { get; private set; }

        readonly object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            return state switch
            {
                State.NotStarted => Initialize(),
                State.BeforeList => MoveNextList(),
                State.EnumeratingList => MoveNextItem(),
                State.Complete => false,
                _ => throw new UnreachableException("Invalid enumerator state reached")
            };
        }

        private bool Initialize()
        {
            Debug.Assert(state == State.NotStarted);
            dictionaryEnumerator = multiMap.dictionary.GetEnumerator();
            state = State.BeforeList;

            return MoveNextList();
        }

        private bool MoveNextList()
        {
            Debug.Assert(state == State.BeforeList);

            if (!dictionaryEnumerator.MoveNext())
            {
                state = State.Complete;
                return false;
            }

            listEnumerator = dictionaryEnumerator.Current.Value.GetEnumerator();
            state = State.EnumeratingList;

            return MoveNextItem();
        }

        private bool MoveNextItem()
        {
            Debug.Assert(state == State.EnumeratingList);

            if (listEnumerator!.MoveNext())
            {
                Current = KeyValuePair.Create(dictionaryEnumerator.Current.Key, listEnumerator.Current);
                return true;
            }

            listEnumerator?.Dispose();
            listEnumerator = null;
            state = State.BeforeList;

            return MoveNextList();
        }

        public void Dispose()
        {
            listEnumerator?.Dispose();
            dictionaryEnumerator.Dispose();
        }

        public void Reset()
        {
            state = State.NotStarted;
        }

        private enum State { NotStarted, BeforeList, EnumeratingList, Complete }
    }
}
