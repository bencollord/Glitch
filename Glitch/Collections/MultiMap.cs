using Glitch.Collections.Extensions;
using Glitch.Linq;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Collections;

public class MultiMap<TKey, TValue> : IMultiMap<TKey, TValue>
    where TKey : notnull
{
    private readonly Dictionary<TKey, IList<TValue>> dictionary;

    public MultiMap() : this(EqualityComparer<TKey>.Default) { }

    public MultiMap(IEqualityComparer<TKey> keyComparer)
    {
        dictionary = new Dictionary<TKey, IList<TValue>>(keyComparer);
    }

    public MultiMap(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        : this(entries, EqualityComparer<TKey>.Default) { }

    public MultiMap(IEnumerable<KeyValuePair<TKey, TValue>> entries, IEqualityComparer<TKey> keyComparer)
        : this(keyComparer)
    {
        AddRange(entries);
    }

    public MultiMap(IDictionary<TKey, IList<TValue>> dictionary)
        : this(new Dictionary<TKey, IList<TValue>>(dictionary)) { }

    public MultiMap(IDictionary<TKey, IList<TValue>> dictionary, IEqualityComparer<TKey> keyComparer)
        : this(new Dictionary<TKey, IList<TValue>>(dictionary, keyComparer)) { }

    public MultiMap(IMultiMap<TKey, TValue> map)
        : this()
    {
        Merge(map);
    }

    public MultiMap(IMultiMap<TKey, TValue> map, IEqualityComparer<TKey> keyComparer) 
        : this(keyComparer)
    {
        Merge(map);
    }

    private MultiMap(Dictionary<TKey, IList<TValue>> dictionary)
    {
        this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
    }

    public int KeyCount => dictionary.Count;

    public int EntryCount => dictionary.Values.Sum(v => v.Count);

    public IEqualityComparer<TKey> Comparer => dictionary.Comparer;

    public IList<TValue> this[TKey key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    public TValue this[TKey key, int index]
    {
        get => dictionary[key][index];
        set => dictionary[key][index] = value;
    }

    public KeyCollection Keys => new(this);

    public ValueCollection Values => new(this);

    public void Add(TKey key, TValue value)
    {
        if (!dictionary.TryGetValue(key, out var list))
        {
            list = [];
            dictionary.Add(key, list);
        }

        list.Add(value);
    }

    public void Add(TKey key, params TValue[] values) => AddRange(key, values);

    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> entries)
    {
        foreach (var (key, value) in entries)
        {
            Add(key, value);
        }
    }

    public void AddRange(TKey key, IEnumerable<TValue> items) => AddRange(key, items.ToList());

    public void AddRange(TKey key, IList<TValue> list)
    {
        if (TryGetList(key, out var existing))
        {
            existing.AddRange(list);
        }
        else
        {
            dictionary.Add(key, list);
        }
    }

    public void Merge(IDictionary<TKey, TValue> dictionary)
    {
        foreach (var key in dictionary.Keys)
        {
            Add(key, dictionary[key]);
        }
    }

    public void Merge(IMultiMap<TKey, TValue> map)
    {
        foreach (var key in map.Keys)
        {
            AddRange(key, map[key]);
        }
    }

    public bool Remove(TKey key, TValue value)
    {
        int index = TryGetList(key, out var list) ? list.IndexOf(value) : -1;

        return index > -1 && RemoveAt(key, index);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key, item.Value);

    public bool RemoveAt(TKey key, int index)
    {
        if (!TryGetList(key, out var existing) || existing.Count <= index)
        {
            return false;
        }

        existing.RemoveAt(index);

        if (existing.Count == 0)
        {
            dictionary.Remove(key);
        }

        return true;
    }

    public int RemoveAll(TKey key)
    {
        if (TryGetList(key, out var list))
        {
            dictionary.Remove(key);
            return list.Count;
        }

        return -1;
    }

    public void Clear() => dictionary.Clear();

    public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);
    
    public bool Contains(KeyValuePair<TKey, TValue> item) => TryGetList(item.Key, out var list) && list.Contains(item.Value);
    
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();
    
    public bool TryGetList(TKey key, [MaybeNullWhen(false)] out IList<TValue> list) => dictionary.TryGetValue(key, out list);

    public bool TryGetValue(TKey key, int index, [NotNullWhen(true)] out TValue? value)
    {
        if (TryGetList(key, out var list) && list.Count < index)
        {
            value = dictionary[key][index];
            return value != null;
        }

        value = default!;
        return false;
    }

    public Enumerator GetEnumerator() => new(this);

    public Dictionary<TKey, IList<TValue>> ToDictionary() => new(dictionary, dictionary.Comparer);

    public IList<TValue> GetOrAddList(TKey key)
    {
        if (!TryGetList(key, out var list))
        {
            list = [];
        }

        return list;
    }

    public int Count(TKey key) => TryGetList(key, out var list) ? list.Count : 0;
    public Lookup<TKey, TValue> ToLookup() => new Lookup<TKey, TValue>(this);

    #region Explicit Interface Implementations
    bool IReadOnlyMultiMap<TKey, TValue>.TryGetList(TKey key, [MaybeNullWhen(false)] out IReadOnlyList<TValue> values)
    {
        if (TryGetList(key, out var list))
        {
            values = list as IReadOnlyList<TValue> ?? new ReadOnlyList<TValue>(list);
            return true;
        }

        values = default;
        return false;
    }

    ILookup<TKey, TValue> IReadOnlyMultiMap<TKey, TValue>.ToLookup() => ToLookup();
    int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => EntryCount;
    int ICollection<KeyValuePair<TKey, TValue>>.Count => EntryCount;
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

    IEnumerable<TKey> IReadOnlyMultiMap<TKey, TValue>.Keys => Keys;
    IEnumerable<TValue> IReadOnlyMultiMap<TKey, TValue>.Values => Values;

    IReadOnlyList<TValue> IReadOnlyMultiMap<TKey, TValue>.this[TKey key] => this[key] as IReadOnlyList<TValue> ?? this[key].ToReadOnlyList();
    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion

    public class KeyCollection : IReadOnlyCollection<TKey>
    {
        private readonly MultiMap<TKey, TValue> map;

        internal KeyCollection(MultiMap<TKey, TValue> map)
        {
            this.map = map;
        }

        public int Count => map.KeyCount;

        public Enumerator GetEnumerator() => new(this);

        IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<TKey>
        {
            private readonly KeyCollection keys;
            private Dictionary<TKey, IList<TValue>>.Enumerator? enumerator;

            internal Enumerator(KeyCollection keys) 
            {
                this.keys = keys;
            }

            public readonly TKey Current => enumerator.GetValueOrDefault().Current.Key;
            readonly object IEnumerator.Current => Current;

            public readonly void Dispose() => enumerator?.Dispose();

            public bool MoveNext()
            {
                if (!enumerator.HasValue)
                {
                    enumerator = keys.map.dictionary.GetEnumerator();
                }

                return enumerator.Value.MoveNext();
            }

            public void Reset() => enumerator = null;
        }
    }

    public class ValueCollection : IReadOnlyCollection<TValue>
    {
        private readonly MultiMap<TKey, TValue> map;

        internal ValueCollection(MultiMap<TKey, TValue> map)
        {
            this.map = map;
        }

        public int Count => map.EntryCount;

        public Enumerator GetEnumerator() => new(this);

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<TValue>
        {
            private readonly ValueCollection values;
            private MultiMap<TKey, TValue>.Enumerator? entryEnumerator;

            internal Enumerator(ValueCollection values)
            {
                this.values = values;
            }

            public readonly TValue Current => entryEnumerator.GetValueOrDefault().Current.Value;
            readonly object? IEnumerator.Current => Current;

            public readonly void Dispose() => entryEnumerator?.Dispose();

            public bool MoveNext()
            {
                if (!entryEnumerator.HasValue)
                {
                    this.entryEnumerator = new MultiMap<TKey, TValue>.Enumerator(this.values.map);
                }

                return entryEnumerator.Value.MoveNext();
            }

            public void Reset() => entryEnumerator = null;
        }
    }

    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private readonly MultiMap<TKey, TValue> multiMap;
        private Dictionary<TKey, IList<TValue>>.Enumerator dictionaryEnumerator;
        private IEnumerator<TValue>? listEnumerator; // TODO Struct enumerator
        private State state = State.NotStarted;

        internal Enumerator(MultiMap<TKey, TValue> multiMap)
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
