using System.Collections.Immutable;

namespace Glitch.Collections.Extensions.Immutable;

public static class ImmutableDictionaryExtensions
{
    extension<TKey, TValue>(IImmutableDictionary<TKey, TValue> dictionary)
        where TKey : notnull
        where TValue : notnull
    {
        public IImmutableDictionary<TValue, TKey> Flip() =>
            dictionary.Flip(EqualityComparer<TValue>.Default);

        public IImmutableDictionary<TValue, TKey> Flip(IEqualityComparer<TValue> comparer) => 
            dictionary.ToImmutableDictionary(e => e.Value, e => e.Key, comparer);
    }
}
