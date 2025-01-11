using Glitch.Linq;
using System.Collections;

namespace Glitch.Collections
{
    public class MultiMap<TKey, TValue> : IDictionary<TKey, IList<TValue>>
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

        public int Count => dictionary.Count;

        public IEnumerable<TKey> Keys => dictionary.Keys;

        public IEnumerable<TValue> Values => dictionary.Values.Flatten();

        public void Add(TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new List<TValue>());
            }
            
            dictionary[key].Add(value);
        }

        public void Add(TKey key, IList<TValue> value) => dictionary.Add(key, value);

        public void Clear() => dictionary.Clear();

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public bool Remove(TKey key) => dictionary.Remove(key);

        public bool TryGetValue(TKey key, out IList<TValue> value) => dictionary.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<TKey, IList<TValue>>> GetEnumerator() => dictionary.GetEnumerator();

        ICollection<TKey> IDictionary<TKey, IList<TValue>>.Keys => dictionary.Keys;
        ICollection<IList<TValue>> IDictionary<TKey, IList<TValue>>.Values => dictionary.Values;
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.IsReadOnly => false;
        void ICollection<KeyValuePair<TKey, IList<TValue>>>.Add(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Add(item);
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Contains(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Contains(item);
        void ICollection<KeyValuePair<TKey, IList<TValue>>>.CopyTo(KeyValuePair<TKey, IList<TValue>>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Remove(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
