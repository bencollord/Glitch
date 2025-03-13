using Glitch.Functional;
using Glitch.Linq;
using System.Collections;

namespace Glitch.Collections
{
    public class MultiMap<TKey, TValue> : IDictionary<TKey, IList<TValue>>, ILookup<TKey, TValue>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, IList<TValue>> dictionary;

        public MultiMap() : this(EqualityComparer<TKey>.Default) { } 

        public MultiMap(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, IList<TValue>>(comparer);
        }

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

        public int Count => dictionary.Count;

        public IEnumerable<TKey> Keys => dictionary.Keys;

        public IEnumerable<TValue> Values => dictionary.Values.Flatten();

        public IList<TValue> Add(TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, []);
            }

            var list = dictionary[key];
            list.Add(value);
            return list;
        }

        public void Add(TKey key, IList<TValue> value) => dictionary.Add(key, value);

        public void Clear() => dictionary.Clear();

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public bool Remove(TKey key) => dictionary.Remove(key);

        public Option<IList<TValue>> TryGetList(TKey key)
            => TryGetList(key, out var list) ? Some(list) : None;

        public Option<TValue> TryGetValue(TKey key, int index)
            => TryGetValue(key, index, out var result) ? Some(result!) : None;

        public bool TryGetList(TKey key, out IList<TValue> list) 
            => dictionary.TryGetValue(key, out list!); // We either ignore nulls here or deal with the "Mismatched type" warning.

        public bool TryGetValue(TKey key, int index, out TValue? value)
        {
            if (TryGetList(key, out IList<TValue> list) && list.Count < index)
            {
                value = dictionary[key][index];
                return true;
            }

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, IList<TValue>>> GetEnumerator() => dictionary.GetEnumerator();

        ICollection<TKey> IDictionary<TKey, IList<TValue>>.Keys => dictionary.Keys;
        ICollection<IList<TValue>> IDictionary<TKey, IList<TValue>>.Values => dictionary.Values;
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.IsReadOnly => false;
        bool IDictionary<TKey, IList<TValue>>.TryGetValue(TKey key, out IList<TValue> list)
            => TryGetList(key, out list);

        IEnumerable<TValue> ILookup<TKey, TValue>.this[TKey key] => this[key];

        void ICollection<KeyValuePair<TKey, IList<TValue>>>.Add(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Add(item);
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Contains(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Contains(item);
        void ICollection<KeyValuePair<TKey, IList<TValue>>>.CopyTo(KeyValuePair<TKey, IList<TValue>>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Remove(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Contains(TKey key) => ContainsKey(key);

        IEnumerator<IGrouping<TKey, TValue>> IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator()
        {
            foreach (var (key, list) in dictionary)
            {
                yield return new Grouping(key, list);
            }
        }

        private record Grouping(TKey Key, IEnumerable<TValue> Values) : IGrouping<TKey, TValue>
        {
            public IEnumerator<TValue> GetEnumerator() => Values.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
