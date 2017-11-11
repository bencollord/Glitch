using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> toAdd)
        {
            Guard.NotNull(source, nameof(source));
            Guard.NotNull(toAdd, nameof(toAdd));

            foreach (KeyValuePair<TKey, TValue> pair in toAdd)
            {
                source.Add(pair);
            }
        }

        public static void MergeInto<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> destination)
        {
            Guard.NotNull(source, nameof(source));
            Guard.NotNull(destination, nameof(destination));

            destination.AddRange(source);
        }
    }
}
