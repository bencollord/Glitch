using Glitch.Functional;
using Glitch.Functional.Results;
using System.Collections;

namespace Glitch.Collections
{
    public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : notnull
    {
        private const int DefaultCapacity = 10;

        private Entry?[] entries;
        private IEqualityComparer<TKey> keyComparer;
        private int count;

        public HashTable() : this(DefaultCapacity) { }

        public HashTable(int capacity)
        {
            entries = new Entry[capacity];
            keyComparer = EqualityComparer<TKey>.Default;
        }

        public TValue this[TKey key]
        {
            get
            {
                return ResultExtensions.ExpectOrElse(FindEntry(key)
                    .Select(x => x.Value)
, _ => Errors.KeyNotFound(key))
                    .Unwrap();
            }
            set
            {
                FindEntry(key)
                    .Do(e => e.Value = value)
                    .IfNone(() => Add(key, value));
            }
        }

        public int Count => count;

        public IEnumerable<TKey> Keys => AllEntries.Select(e => e.Key);

        public IEnumerable<TValue> Values => AllEntries.Select(e => e.Value);

        private IEnumerable<Entry> AllEntries
            => entries.Where(e => e is not null)
                      .SelectMany(e => e!.Flatten());

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys.ToList().AsReadOnly();
        ICollection<TValue> IDictionary<TKey, TValue>.Values => Values.ToList().AsReadOnly();

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (count >= entries.Length)
            {
                Resize();
            }

            int hashCode = keyComparer.GetHashCode(key);
            int bucket = GetBucket(hashCode);

            Entry? entry = entries[bucket];

            if (entry is null)
            {
                entries[bucket] = new Entry(hashCode, key, value);
                ++count;
                return;
            }

            while (entry.Next is not null)
            {
                if (keyComparer.Equals(entry.Key, key))
                {
                    throw new ArgumentException("Duplicate key");
                }

                entry = entry.Next;
            }

            entry.Next = new Entry(hashCode, key, value);
            ++count;
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            using var iter = GetEnumerator();
            int i = arrayIndex;

            while (iter.MoveNext())
            {
                array[i++] = iter.Current;
            }
        }

        public Enumerator GetEnumerator() => new(this);

        public bool Remove(TKey key)
        {
            int bucket = GetBucket(keyComparer.GetHashCode(key));
            Entry? entry = entries[bucket];
            Entry? prev = null;

            while (entry is not null)
            {
                if (keyComparer.Equals(key, entry.Key))
                {
                    if (prev is not null)
                    {
                        prev.Next = entry.Next;
                    }
                    else
                    {
                        entries[bucket] = entry.Next;
                    }

                    count--;
                    return true;
                }
                else
                {
                    prev = entry;
                    entry = entry.Next;
                    continue;
                }
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool isMatch = Contains(item);

            if (isMatch)
            {
                Remove(item.Key);
            }

            return isMatch;
        }

        public void Clear()
        {
            entries = new Entry[DefaultCapacity];
            count = 0;
        }

        public bool ContainsKey(TKey key) => FindEntry(key).IsSome;

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return FindEntry(item.Key)
                .Where(e => EqualityComparer<TValue>.Default.Equals(e.Value, item.Value))
                .IsSome;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var entry = FindEntry(key);

            value = entry.Select(e => e.Value).DefaultIfNone()!;

            return entry.IsSome;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Resize()
        {
            var newEntries = new Entry[entries.Length * 2];

            var newEntryGroups = entries
                .Where(e => e is not null)
                .SelectMany(e => e!.Flatten())
                .GroupBy(e => e.HashCode % newEntries.Length)
                .Select(e => new
                {
                    Bucket = e.Key,
                    Entries = e
                });

            foreach (var grp in newEntryGroups)
            {
                var iter = grp.Entries.GetEnumerator();

                if (!iter.MoveNext())
                {
                    continue;
                }

                newEntries[grp.Bucket] = iter.Current;
                var prev = iter.Current;

                while (iter.MoveNext())
                {
                    prev.Next = iter.Current;
                    prev = iter.Current;
                }
            }
        }

        private Option<Entry> FindEntry(TKey key)
        {
            int bucket = GetBucket(key);
            Entry? entry = entries[bucket];

            while (entry is not null)
            {
                if (keyComparer.Equals(entry.Key, key))
                {
                    return entry;
                }

                entry = entry.Next;
            }

            return None;
        }

        private int GetBucket(TKey key) => GetBucket(keyComparer.GetHashCode(key));

        private int GetBucket(int hashCode) => Math.Abs(hashCode % entries.Length);

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private Entry[] entries;
            private int cursor = 0;
            private Option<Entry> current;

            internal Enumerator(HashTable<TKey, TValue> hashTable)
            {
                entries = hashTable.entries!;
                current = None;
            }

            public KeyValuePair<TKey, TValue> Current
                => current.Select(e => e.ToKeyValuePair())
                          .IfNone(default(KeyValuePair<TKey, TValue>));

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (cursor >= entries.Length)
                {
                    return false;
                }

                current = current.AndThen(e => Maybe(e.Next));

                if (current.IsSome)
                {
                    return true;
                }

                while (current.IsNone && cursor < entries.Length)
                {
                    current = entries[cursor++];
                }

                return current.IsSome;
            }

            public void Dispose()
            {
                entries = new Entry[0];
                current = None;
                cursor = -1;
            }

            void IEnumerator.Reset()
            {
                if (cursor < 0)
                {
                    throw new ObjectDisposedException(nameof(Enumerator));
                }

                cursor = 0;
                current = None;
            }
        }


        private class Entry
        {
            private TKey key;
            private TValue value;
            private int hashCode;
            private Entry? next;

            internal Entry(int hashCode, TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                this.hashCode = hashCode;
            }

            internal TKey Key => key;

            internal int HashCode => hashCode;

            internal TValue Value
            {
                get => value;
                set => this.value = value;
            }

            internal Entry? Next
            {
                get => next;
                set => next = value;
            }

            internal KeyValuePair<TKey, TValue> ToKeyValuePair() => new KeyValuePair<TKey, TValue>(key, value);

            internal IEnumerable<Entry> Flatten()
            {
                var current = this;

                do
                {
                    yield return current;
                    current = current.next;
                }
                while (current is not null);
            }
        }
    }
}
