using Glitch.Functional;
using System.Collections;
using System.Collections.Immutable;

namespace Glitch.Collections
{
    #pragma warning disable IDE0305 // Simplify collection initialization
    #pragma warning disable IDE0306 // Simplify collection initialization
    #pragma warning disable IDE0303 // Simplify collection initialization
    public partial class ImmutableMultiMap<TKey, TValue>
    {
        public class Builder : IMultiMap<TKey, TValue>
        {
            private Option<ImmutableMultiMap<TKey, TValue>> immutable;
            private Lazy<MultiMap<TKey, TValue>> mutable;
            private Option<IEqualityComparer<TKey>> keyComparer;

            internal Builder(ImmutableMultiMap<TKey, TValue> immutable)
            {
                this.immutable = immutable;
                mutable = new(InitMutable);
                keyComparer = Maybe(immutable.Comparer);
            }

            internal Builder(IEqualityComparer<TKey> keyComparer)
            {
                this.keyComparer = Maybe(keyComparer);
                immutable = Empty;
                mutable = new(InitMutable);
            }

            public int KeyCount => mutable.Value.KeyCount;

            public int ValueCount => mutable.Value.ValueCount;

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

            ICollection<TKey> IDictionary<TKey, IList<TValue>>.Keys => ((IDictionary<TKey, IList<TValue>>)mutable.Value).Keys;

            ICollection<IList<TValue>> IDictionary<TKey, IList<TValue>>.Values => ((IDictionary<TKey, IList<TValue>>)mutable.Value).Values;

            int ICollection<KeyValuePair<TKey, IList<TValue>>>.Count => ((IDictionary<TKey, IList<TValue>>)mutable.Value).Count;

            bool ICollection<KeyValuePair<TKey, IList<TValue>>>.IsReadOnly => ((IDictionary<TKey, IList<TValue>>)mutable.Value).IsReadOnly;

            public ImmutableMultiMap<TKey, TValue> ToImmutable()
            {
                // If no properties have been accessed, the original never changed
                if (!mutable.IsValueCreated)
                {
                    return immutable.IfNone(Empty);
                }

                var dictionary = mutable.Value
                    .ToDictionary()
                    .ToImmutableDictionary(
                        pair => pair.Key,
                        pair => (IImmutableList<TValue>)ImmutableList.CreateRange(pair.Value),
                        Comparer);

                return new(dictionary);
            }

            public IList<TValue> Add(TKey key, TValue value) => mutable.Value.Add(key, value);

            public IList<TValue> Add(TKey key, params TValue[] values) => mutable.Value.AddRange(key, values);

            public IList<TValue> AddRange(TKey key, IEnumerable<TValue> values) => mutable.Value.AddRange(key, values);

            public IList<TValue> AddRange(TKey key, IList<TValue> list) => mutable.Value.AddRange(key, list);
            
            public void Clear() => mutable.Value.Clear();

            public bool ContainsKey(TKey key) => mutable.Value.ContainsKey(key);

            public bool Remove(TKey key) => mutable.Value.Remove(key);

            public Option<IList<TValue>> TryGetList(TKey key) => mutable.Value.TryGetList(key);

            public Option<TValue> TryGetValue(TKey key, int index) => mutable.Value.TryGetValue(key, index);

            public bool TryGetList(TKey key, out IList<TValue> list) => mutable.Value.TryGetList(key, out list);

            public bool TryGetValue(TKey key, int index, out TValue? value) => mutable.Value.TryGetValue(key, index, out value);

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => mutable.Value.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)mutable.Value).GetEnumerator();
            }

            private MultiMap<TKey, TValue> InitMutable()
            {
                var comparer = keyComparer
                    .OrElse(() => immutable.Map(i => i.Comparer))
                    .IfNone(EqualityComparer<TKey>.Default);


                var map = new MultiMap<TKey, TValue>(comparer);
                
                if (immutable.IsNoneOr(i => i.dictionary.Count == 0))
                {
                    return map;
                }

                foreach (var (key, list) in immutable.Iterate().SelectMany(i => i.dictionary))
                {
                    map.AddRange(key, list.ToList());
                }

                return map;
            }

            void IDictionary<TKey, IList<TValue>>.Add(TKey key, IList<TValue> value)
            {
                ((IDictionary<TKey, IList<TValue>>)mutable.Value).Add(key, value);
            }

            bool IDictionary<TKey, IList<TValue>>.TryGetValue(TKey key, out IList<TValue> value)
            {
                return ((IDictionary<TKey, IList<TValue>>)mutable.Value).TryGetValue(key, out value!);
            }

            void ICollection<KeyValuePair<TKey, IList<TValue>>>.Add(KeyValuePair<TKey, IList<TValue>> item)
            {
                ((IDictionary<TKey, IList<TValue>>)mutable.Value).Add(item);
            }

            bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Contains(KeyValuePair<TKey, IList<TValue>> item)
            {
                return ((IDictionary<TKey, IList<TValue>>)mutable.Value).Contains(item);
            }

            void ICollection<KeyValuePair<TKey, IList<TValue>>>.CopyTo(KeyValuePair<TKey, IList<TValue>>[] array, int arrayIndex)
            {
                ((IDictionary<TKey, IList<TValue>>)mutable.Value).CopyTo(array, arrayIndex);
            }

            bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Remove(KeyValuePair<TKey, IList<TValue>> item)
            {
                return ((IDictionary<TKey, IList<TValue>>)mutable.Value).Remove(item);
            }

            IEnumerator<KeyValuePair<TKey, IList<TValue>>> IEnumerable<KeyValuePair<TKey, IList<TValue>>>.GetEnumerator()
            {
                return ((IDictionary<TKey, IList<TValue>>)mutable.Value).GetEnumerator();
            }
        }
    }
}
