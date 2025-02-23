namespace Glitch.Functional
{
    public static class CollectionExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            return dictionary.TryGetValue(key, out var value) ? value : None;
        }

        public static Option<TValue> TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            return dictionary.TryGetValue(key, out var value) ? value : None;
        }

        // Disambiguate for main dictionary since it implements both of the above interfaces
        public static Option<TValue> TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
            where TKey : notnull
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            return dictionary.TryGetValue(key, out var value) ? value : None;
        }

        public static Option<T> TryPop<T>(this Stack<T> stack)
        {
            return stack.TryPop(out var value) ? value : None;
        }

        public static Option<T> TryPeek<T>(this Stack<T> stack)
        {
            return stack.TryPeek(out var value) ? value : None;
        }

        public static Option<T> TryDequeue<T>(this Queue<T> queue)
        {
            return queue.TryDequeue(out var value) ? value : None;
        }

        public static Option<T> TryPeek<T>(this Queue<T> queue)
        {
            return queue.TryPeek(out var value) ? value : None;
        }
    }
}
