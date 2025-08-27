using Glitch.Functional;
using Glitch.Functional.Results;
using Glitch.Linq;
using System.Collections;
using System.Collections.Immutable;

namespace Glitch.Collections
{
    public static class ImmutableMultiMap
    {
        public static ImmutableMultiMap<TKey, TValue> Create<TKey, TValue>(IReadOnlyMultiMap<TKey, TValue> multiMap)
            where TKey : notnull
            => multiMap.Keys.Select(k => KeyValuePair.Create(k, multiMap[k].ToImmutableList() as IImmutableList<TValue>))
                            .ToImmutableDictionary()
                            .PipeInto(dict => new ImmutableMultiMap<TKey, TValue>(dict));

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

            return source.ToLookup(keySelector, elementSelector, keyComparer)
                         .PipeInto(Create);
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

            return source.GroupBy(s => s.Key, s => s.Value, keyComparer)
                         .PipeInto(s => Create(s, keyComparer));
        }

        public static ImmutableMultiMap<TKey, TValue> ToImmutableMultiMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) 
            where TKey : notnull
        {
            return ToImmutableMultiMap(source, null);
        }

        private static KeyValuePair<TKey, IImmutableList<TValue>> CreatePair<TKey, TValue>(TKey key, IEnumerable<TValue> values)
            => new(key, values.ToImmutableList());
    }

    public partial class ImmutableMultiMap<TKey, TValue>
        : IImmutableDictionary<TKey, IImmutableList<TValue>>, 
          IReadOnlyMultiMap<TKey, TValue>,
          IEnumerable<KeyValuePair<TKey, TValue>>
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

        public int ValueCount => dictionary.Values.Sum(e => e.Count);

        public ImmutableMultiMap<TKey, TValue> WithComparer(IEqualityComparer<TKey>? comparer)
            => new(dictionary.WithComparers(comparer));

        public ImmutableMultiMap<TKey, TValue> Add(TKey key, TValue value)
        {
            return TryGetList(key)
                .Map(list => list.Add(value))
                .Map(list => SetList(key, list))
                .IfNone(this);
        }

        public ImmutableMultiMap<TKey, TValue> Add(TKey key, params TValue[] values) => AddRange(key, values);

        public ImmutableMultiMap<TKey, TValue> AddRange(TKey key, IEnumerable<TValue> values) => AddRange(key, ImmutableList.CreateRange(values));

        public ImmutableMultiMap<TKey, TValue> AddRange(TKey key, IImmutableList<TValue> list)
            => TryGetList(key)
                   .Map(existing => dictionary.SetItem(key, existing.AddRange(list)))
                   .IfNone(() => dictionary.Add(key, list))
                   .PipeInto(dict => new ImmutableMultiMap<TKey, TValue>(dict));

        public ImmutableMultiMap<TKey, TValue> Clear() => new(dictionary.Clear());

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary
                .SelectMany(pair => pair.Value, (p, v) => KeyValuePair.Create(p.Key, v))
                .GetEnumerator();
        }

        public Builder ToBuilder() => new(this);

        public ImmutableMultiMap<TKey, TValue> Remove(TKey key) => new(dictionary.Remove(key));
        
        public ImmutableMultiMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys) => new(dictionary.RemoveRange(keys));
        
        public ImmutableMultiMap<TKey, TValue> SetItem(TKey key, int index, TValue item) => new(dictionary.SetItem(key, dictionary[key].SetItem(index, item)));
        
        public ImmutableMultiMap<TKey, TValue> SetList(TKey key, IImmutableList<TValue> list) => new(dictionary.SetItem(key, list));

        public Option<TKey> TryGetKey(TKey key) => TryGetKey(key, out TKey actualKey) ? actualKey : None;

        public bool TryGetKey(TKey key, out TKey actualKey) => dictionary.TryGetKey(key, out actualKey);

        public Option<TValue> TryGetValue(TKey key, int index) 
            => from list in TryGetList(key)
               where list.Count > index
               select list[index];

        public bool TryGetValue(TKey key, int index, out TValue value)
        {
            var option = TryGetValue(key, index);
            value = option.UnwrapOrDefault()!; // Might be null, but the compiler will complain for every client if
                                               // we don't shut it up now. You know the deal if you see a method
            return option.IsSome;              // with this convention.
        }

        public Option<IImmutableList<TValue>> TryGetList(TKey key) => TryGetList(key, out var list) ? Some(list!) : None;
        
        public bool TryGetList(TKey key, out IImmutableList<TValue> list) => dictionary.TryGetValue(key, out list!);

        IEnumerable<IImmutableList<TValue>> IReadOnlyDictionary<TKey, IImmutableList<TValue>>.Values => dictionary.Values;
        int IReadOnlyCollection<KeyValuePair<TKey, IImmutableList<TValue>>>.Count => dictionary.Count;

        IEnumerable<IEnumerable<TValue>> IReadOnlyDictionary<TKey, IEnumerable<TValue>>.Values => dictionary.Values;

        int IReadOnlyCollection<KeyValuePair<TKey, IEnumerable<TValue>>>.Count => dictionary.Count;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, IEnumerable<TValue>>.this[TKey key] => this[key];

        IImmutableDictionary<TKey, IImmutableList<TValue>> IImmutableDictionary<TKey, IImmutableList<TValue>>.Remove(TKey key) => Remove(key);
        IImmutableDictionary<TKey, IImmutableList<TValue>> IImmutableDictionary<TKey, IImmutableList<TValue>>.RemoveRange(IEnumerable<TKey> keys) => RemoveRange(keys);
        IImmutableDictionary<TKey, IImmutableList<TValue>> IImmutableDictionary<TKey, IImmutableList<TValue>>.SetItem(TKey key, IImmutableList<TValue> value) => SetList(key, value);
        IImmutableDictionary<TKey, IImmutableList<TValue>> IImmutableDictionary<TKey, IImmutableList<TValue>>.SetItems(IEnumerable<KeyValuePair<TKey, IImmutableList<TValue>>> items) => new ImmutableMultiMap<TKey, TValue>(dictionary.SetItems(items));
        bool IReadOnlyDictionary<TKey, IImmutableList<TValue>>.TryGetValue(TKey key, out IImmutableList<TValue> value) => TryGetList(key, out value!);
        IImmutableDictionary<TKey, IImmutableList<TValue>> IImmutableDictionary<TKey, IImmutableList<TValue>>.Add(TKey key, IImmutableList<TValue> value) => AddRange(key, value);
        IImmutableDictionary<TKey, IImmutableList<TValue>> IImmutableDictionary<TKey, IImmutableList<TValue>>.AddRange(IEnumerable<KeyValuePair<TKey, IImmutableList<TValue>>> pairs) => new ImmutableMultiMap<TKey, TValue>(dictionary.AddRange(pairs));
        IImmutableDictionary<TKey, IImmutableList<TValue>> IImmutableDictionary<TKey, IImmutableList<TValue>>.Clear() => Clear();
        bool IImmutableDictionary<TKey, IImmutableList<TValue>>.Contains(KeyValuePair<TKey, IImmutableList<TValue>> pair) => dictionary.Contains(pair);
        IEnumerator<KeyValuePair<TKey, IImmutableList<TValue>>> IEnumerable<KeyValuePair<TKey, IImmutableList<TValue>>>.GetEnumerator() => dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
            => dictionary.Select(p => KeyValuePair.Create(p.Key, p.Value.AsEnumerable())).GetEnumerator();
    }
}
