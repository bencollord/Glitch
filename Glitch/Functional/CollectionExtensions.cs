namespace Glitch.Functional
{
    public static class CollectionExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            return dictionary.TryGetValue(key, out var value) ? value : Option.None;
        }
    }
}
