using Glitch.Diagnostics;
using Glitch.Functional;
using Glitch.Functional.Results;
using Glitch.Linq;
using System.Collections;
using System.Diagnostics;

namespace Glitch.Collections
{
    public class MultiMap<TKey, TValue>
        : IDictionary<TKey, IList<TValue>>,
          IReadOnlyMultiMap<TKey, TValue>,
          ILookup<TKey, TValue>,
          IEnumerable<KeyValuePair<TKey, TValue>>, 
          IMultiMap<TKey, TValue> 
             where TKey : notnull
    {
        private readonly Dictionary<TKey, IList<TValue>> dictionary;

        public MultiMap() : this(EqualityComparer<TKey>.Default) { }

        public MultiMap(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, IList<TValue>>(comparer);
        }

        public MultiMap(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
            : this(pairs, EqualityComparer<TKey>.Default) { }

        public MultiMap(IEnumerable<KeyValuePair<TKey, TValue>> pairs, IEqualityComparer<TKey> comparer)
        {
            dictionary = pairs
                .GroupBy(p => p.Key, p => p.Value, comparer)
                .ToDictionary(g => g.Key, g => (IList<TValue>)g.ToList(), comparer);
        }

        public MultiMap(IDictionary<TKey, IList<TValue>> dictionary)
            : this(new Dictionary<TKey, IList<TValue>>(dictionary)) { }

        public MultiMap(IDictionary<TKey, IList<TValue>> dictionary, IEqualityComparer<TKey> comparer)
            : this(new Dictionary<TKey, IList<TValue>>(dictionary, comparer)) { }

        private MultiMap(Dictionary<TKey, IList<TValue>> dictionary)
        {
            this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        public int KeyCount => dictionary.Count;

        public int ValueCount => dictionary.Values.Sum(v => v.Count);

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

        public IList<TValue> Add(TKey key, params TValue[] values) => AddRange(key, values);

        public IList<TValue> AddRange(TKey key, IEnumerable<TValue> items)
            => AddRange(key, items.ToList());

        public IList<TValue> AddRange(TKey key, IList<TValue> list)
        {
            return TryGetList(key)
                .Do(existing => existing.AddRange(list))
                .IfNone(() => dictionary.Add(key, list))
                .UnwrapOr(list);
        }

        public void Clear() => dictionary.Clear();

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public bool Remove(TKey key) => dictionary.Remove(key);

        public Option<IList<TValue>> TryGetList(TKey key)
            => TryGetList(key, out var list) ? Some(list) : None;

        public Option<TValue> TryGetValue(TKey key, int index)
            => TryGetValue(key, index, out var result) ? Some(result!) : None;

        public bool TryGetList(TKey key, out IList<TValue> list)
            => dictionary.TryGetValue(key, out list!); // We either ignore nulls here or deal with the "Mismatched type" warning.

        public bool TryGetValue(TKey key, int index, out TValue value)
        {
            if (TryGetList(key, out IList<TValue> list) && list.Count < index)
            {
                value = dictionary[key][index];
                return true;
            }

            value = default!;
            return false;
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        public Dictionary<TKey, IList<TValue>> ToDictionary() => new(dictionary, dictionary.Comparer);

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private MultiMap<TKey, TValue> multiMap;
            private Dictionary<TKey, IList<TValue>>.Enumerator dictionaryEnumerator;
            private IEnumerator<TValue>? listEnumerator;
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
                    _ => GlitchDebug.Fail(false, "Invalid enumerator state reached")
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

        #region Explicit Interface Implementations

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => GetEnumerator();

        IEnumerator<IGrouping<TKey, TValue>> IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator()
        {
            foreach (var (key, list) in dictionary)
            {
                yield return new Grouping(key, list);
            }
        }
        IEnumerator<KeyValuePair<TKey, IList<TValue>>> IEnumerable<KeyValuePair<TKey, IList<TValue>>>.GetEnumerator() => dictionary.GetEnumerator();
        #endregion

        #region IDictionary
        void IDictionary<TKey, IList<TValue>>.Add(TKey key, IList<TValue> list) => AddRange(key, list);
        bool IDictionary<TKey, IList<TValue>>.TryGetValue(TKey key, out IList<TValue> list) => TryGetList(key, out list);
        #endregion

        #region ILookup
        int ILookup<TKey, TValue>.Count => dictionary.Count;
        IEnumerable<TValue> ILookup<TKey, TValue>.this[TKey key] => this[key];
        bool ILookup<TKey, TValue>.Contains(TKey key) => ContainsKey(key);
        #endregion

        #region ICollection
        int ICollection<KeyValuePair<TKey, IList<TValue>>>.Count => KeyCount;
        ICollection<TKey> IDictionary<TKey, IList<TValue>>.Keys => dictionary.Keys;
        ICollection<IList<TValue>> IDictionary<TKey, IList<TValue>>.Values => dictionary.Values;
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.IsReadOnly => false;

        IEnumerable<IEnumerable<TValue>> IReadOnlyDictionary<TKey, IEnumerable<TValue>>.Values => dictionary.Values;

        int IReadOnlyCollection<KeyValuePair<TKey, IEnumerable<TValue>>>.Count => KeyCount;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, IEnumerable<TValue>>.this[TKey key] => this[key];

        void ICollection<KeyValuePair<TKey, IList<TValue>>>.Add(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Add(item);
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Contains(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Contains(item);
        void ICollection<KeyValuePair<TKey, IList<TValue>>>.CopyTo(KeyValuePair<TKey, IList<TValue>>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<TKey, IList<TValue>>>.Remove(KeyValuePair<TKey, IList<TValue>> item) => ((ICollection<KeyValuePair<TKey, IList<TValue>>>)dictionary).Remove(item);

        bool IReadOnlyMultiMap<TKey, TValue>.TryGetList(TKey key, out IEnumerable<TValue> list)
        {
            if (TryGetList(key, out var stupidHackyCantBelieveOutParametersArentCovariantList))
            {
                list = stupidHackyCantBelieveOutParametersArentCovariantList;
                return true;
            }

            list = [];
            return false;
        }

        bool IReadOnlyDictionary<TKey, IEnumerable<TValue>>.TryGetValue(TKey key, out IEnumerable<TValue> value)
            => ((IReadOnlyMultiMap<TKey, TValue>)this).TryGetList(key, out value);


        IEnumerator<KeyValuePair<TKey, IEnumerable<TValue>>> IEnumerable<KeyValuePair<TKey, IEnumerable<TValue>>>.GetEnumerator()
        {
            foreach (var (key, value) in dictionary)
            {
                yield return KeyValuePair.Create(key, value.AsEnumerable());
            }
        }

        #endregion

        #endregion

        private record Grouping(TKey Key, IEnumerable<TValue> Values) : IGrouping<TKey, TValue>
        {
            public IEnumerator<TValue> GetEnumerator() => Values.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
