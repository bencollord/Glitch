using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Collections;

public partial class ImmutableMultiMap<TKey, TValue>
{
    public class Builder : IMultiMap<TKey, TValue>
    {
        private ImmutableMultiMap<TKey, TValue>? immutable;
        private Lazy<MultiMap<TKey, TValue>> mutable;
        private IEqualityComparer<TKey>? keyComparer;

        internal Builder(ImmutableMultiMap<TKey, TValue>? immutable)
        {
            this.immutable = immutable;
            mutable = new(InitMutable);
            keyComparer = immutable?.Comparer;
        }

        internal Builder(IEqualityComparer<TKey>? keyComparer)
        {
            this.keyComparer = keyComparer;
            immutable = null;
            mutable = new(InitMutable);
        }

        public int KeyCount => mutable.Value.KeyCount;

        public int EntryCount => mutable.Value.EntryCount;

        public IEqualityComparer<TKey> Comparer => mutable.Value.Comparer;

        public IList<TValue> this[TKey key]
        {
            get => mutable.Value[key];
            set => mutable.Value[key] = value;
        }

        public TValue this[TKey key, int index]
        {
            get => mutable.Value[key, index];
            set => mutable.Value[key, index] = value;
        }

        public IEnumerable<TKey> Keys => mutable.Value.Keys;

        public IEnumerable<TValue> Values => mutable.Value.Values;

        public ImmutableMultiMap<TKey, TValue> ToImmutable()
        {
            // If no properties have been accessed, the original never changed
            if (!mutable.IsValueCreated)
            {
                return immutable ?? Empty;
            }

            var dictionary = mutable.Value
                .ToDictionary()
                .ToImmutableDictionary(
                    pair => pair.Key,
                    pair => (IImmutableList<TValue>)[..pair.Value],
                    Comparer);

            return new(dictionary);
        }

        public void Add(TKey key, TValue value) => mutable.Value.Add(key, value);
        public void Add(KeyValuePair<TKey, TValue> item) => mutable.Value.Add(item);
        public void Add(TKey key, params TValue[] values) => mutable.Value.AddRange(key, values);
        public void AddRange(TKey key, IEnumerable<TValue> values) => mutable.Value.AddRange(key, values);
        public void AddRange(TKey key, IList<TValue> list) => mutable.Value.AddRange(key, list);
        
        public int Count(TKey key) => mutable.Value.Count(key);
        public bool ContainsKey(TKey key) => mutable.Value.ContainsKey(key);
        public bool Contains(KeyValuePair<TKey, TValue> item) => mutable.Value.Contains(item);

        public bool Remove(TKey key, TValue value) => mutable.Value.Remove(key, value);
        public bool Remove(KeyValuePair<TKey, TValue> item) => mutable.Value.Remove(item);
        public bool RemoveAt(TKey key, int index) => mutable.Value.RemoveAt(key, index);
        public int RemoveAll(TKey key) => mutable.Value.RemoveAll(key);

        public void Clear() => mutable.Value.Clear();

        public IList<TValue> GetOrAddList(TKey key) => mutable.Value.GetOrAddList(key);

        public ILookup<TKey, TValue> ToLookup() => mutable.Value.ToLookup();

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => mutable.Value.CopyTo(array, arrayIndex);

        public bool TryGetList(TKey key, out IList<TValue> list) => mutable.Value.TryGetList(key, out list);

        public bool TryGetValue(TKey key, int index, out TValue? value) => mutable.Value.TryGetValue(key, index, out value);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => mutable.Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IReadOnlyList<TValue> IReadOnlyMultiMap<TKey, TValue>.this[TKey key] => this[key] as IReadOnlyList<TValue> ?? this[key].ToReadOnlyList();
        int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => EntryCount;
        int ICollection<KeyValuePair<TKey, TValue>>.Count => EntryCount;
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        private MultiMap<TKey, TValue> InitMutable()
        {
            var comparer = keyComparer
                ?? immutable?.Comparer
                ?? EqualityComparer<TKey>.Default;

            var map = new MultiMap<TKey, TValue>(comparer);
            
            if (immutable is null || immutable.dictionary.Count == 0)
            {
                return map;
            }

            foreach (var (key, list) in immutable.dictionary)
            {
                map.AddRange(key, list.ToList());
            }

            return map;
        }

        bool IReadOnlyMultiMap<TKey, TValue>.TryGetList(TKey key, out IReadOnlyList<TValue> list) => throw new NotImplementedException();
    }
}
