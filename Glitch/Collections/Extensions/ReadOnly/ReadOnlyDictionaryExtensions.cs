namespace Glitch.Collections.Extensions.ReadOnly;

public static class ReadOnlyDictionaryExtensions
{
    extension<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary)
        where TKey : notnull
        where TValue : notnull
    {
        public IReadOnlyDictionary<TValue, TKey> Flip() =>
            dictionary.Flip(EqualityComparer<TValue>.Default);

        public IReadOnlyDictionary<TValue, TKey> Flip(IEqualityComparer<TValue> comparer) => 
            dictionary.ToDictionary(e => e.Value, e => e.Key, comparer);
    }
}
