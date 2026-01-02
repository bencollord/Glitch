using System.Collections;

namespace Glitch.Collections;

public readonly record struct Grouping<TKey, TValue>(TKey Key, IEnumerable<TValue> Values) : IGrouping<TKey, TValue>
        where TKey : notnull
    {
        public IEnumerator<TValue> GetEnumerator() => Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
