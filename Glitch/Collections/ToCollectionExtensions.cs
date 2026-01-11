namespace Glitch.Collections;

public static class ToCollectionExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public ReadOnlyList<T> ToReadOnlyList() => new(source);

        public Deque<T> ToDeque() => new(source);

        public MultiMap<TKey, T> ToMultiMap<TKey>(Func<T, TKey> keySelector)
            where TKey : notnull =>
            source.Select(x => KeyValuePair.Create(keySelector(x), x))
                  .ToMultiMap();

        public MultiMap<TKey, T> ToMultiMap<TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
            where TKey : notnull =>
            source.Select(x => KeyValuePair.Create(keySelector(x), x))
                  .ToMultiMap(keyComparer);

        public MultiMap<TKey, TValue> ToMultiMap<TKey, TValue>(Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
            where TKey : notnull =>
            source.Select(x => KeyValuePair.Create(keySelector(x), valueSelector(x)))
                  .ToMultiMap();

        public MultiMap<TKey, TValue> ToMultiMap<TKey, TValue>(Func<T, TKey> keySelector, Func<T, TValue> valueSelector, IEqualityComparer<TKey> keyComparer)
            where TKey : notnull =>
            source.Select(x => KeyValuePair.Create(keySelector(x), valueSelector(x)))
                  .ToMultiMap(keyComparer);

        public KeyedStack<TKey, TValue> ToKeyedStack<TKey, TValue>(Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
            where TKey : notnull =>
            source.Select(x => KeyValuePair.Create(keySelector(x), valueSelector(x)))
                  .ToKeyedStack();

        public KeyedStack<TKey, TValue> ToKeyedStack<TKey, TValue>(Func<T, TKey> keySelector, Func<T, TValue> valueSelector, IEqualityComparer<TKey> keyComparer)
            where TKey : notnull =>
            source.Select(x => KeyValuePair.Create(keySelector(x), valueSelector(x)))
                  .ToKeyedStack(keyComparer);
    }

    extension<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source)
        where TKey : notnull
    {
        public MultiMap<TKey, TValue> ToMultiMap() => new(source);

        public MultiMap<TKey, TValue> ToMultiMap(IEqualityComparer<TKey> keyComparer) => new(source, keyComparer);

        public KeyedStack<TKey, TValue> ToKeyedStack() => new(source);

        public KeyedStack<TKey, TValue> ToKeyedStack(IEqualityComparer<TKey> keyComparer) => new(source, keyComparer);
    }
}
