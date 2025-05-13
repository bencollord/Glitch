namespace Glitch.Collections
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection is List<T> list)
            {
                list.AddRange(items);
                return;
            }

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void Merge<TKey, TValue, TMerge>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TMerge> other)
            where TMerge : TValue
            => dictionary.Merge(other, MergeOption.Strict);

        public static void Merge<TKey, TValue, TMerge>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TMerge> other, MergeOption mergeOption)
            where TMerge : TValue
        {
            foreach (var (key, value) in other)
            {
                switch (mergeOption)
                {
                    case MergeOption.Ignore:
                        dictionary.TryAdd(key, value);
                        break;

                    case MergeOption.Overwrite:
                        dictionary[key] = value;
                        break;

                    case MergeOption.Strict:
                        dictionary.Add(key, value);
                        break;

                    default:
                        throw new ArgumentException($"Invalid merge option {mergeOption}");
                }
            }
        }

        public static Dictionary<TValue, TKey> Flip<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
            where TValue : notnull
            => dictionary.ToDictionary(p => p.Value, p => p.Key);
    }
}
