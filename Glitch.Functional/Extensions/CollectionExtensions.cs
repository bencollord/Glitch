using Glitch.Functional;
using System.Collections;
using System.Collections.Immutable;

namespace Glitch.Functional;

using static Option;

public static class CollectionExtensions
{
    public static Option<TValue> TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
    {
        ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        return dictionary.TryGetValue(key, out var value) ? value : None;
    }

    public static Option<TValue> TryGetValue<TKey, TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key)
    {
        ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        return dictionary.TryGetValue(key, out var value) ? value : None;
    }

    // Disambiguate for common concrete classes that implement both of the above interfaces
    public static Option<TValue> TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        return dictionary.TryGetValue(key, out var value) ? value : None;
    }

    public static Option<TValue> TryGetValue<TKey, TValue>(this ImmutableDictionary<TKey, TValue> dictionary, TKey key)
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

    /// <summary>
    /// Forces enumeration of the enumerable, consuming it.
    /// </summary>
    /// <remarks>
    /// This method is useful when you have a sequence of lazily evauated
    /// monads or delegates that have side effects you want to execute 
    /// even though you don't neeed anything out of the sequence.
    /// </remarks>
    /// <param name="source"></param>
    /// <returns>Unit</returns>
    public static Unit Consume(this IEnumerable source)
    {
        foreach (var _ in source)
        {
        }

        return Unit.Value;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Func<T, Unit> action)
    {
        foreach (var item in source)
        {
            action(item);
        }

        return source;
    }
}
