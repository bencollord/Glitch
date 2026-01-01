using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Glitch.Collections;

public class KeyedStack<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    where TKey : notnull
{
    private Dictionary<TKey, Stack<TValue>> map;

    public KeyedStack() 
        : this(EqualityComparer<TKey>.Default) { }

    public KeyedStack(IEqualityComparer<TKey> keyComparer)
    {
        map = new Dictionary<TKey, Stack<TValue>>(keyComparer);
    }

    public KeyedStack(IEnumerable<KeyValuePair<TKey, TValue>> entries) 
        : this(entries, EqualityComparer<TKey>.Default) { }

    public KeyedStack(IEnumerable<KeyValuePair<TKey, TValue>> entries, IEqualityComparer<TKey> keyComparer)
    {
        map = entries.GroupBy(e => e.Key, e => e.Value, keyComparer)
                     .Select(g => KeyValuePair.Create(g.Key, new Stack<TValue>(g)))
                     .ToDictionary();
    }

    public int KeyCount => map.Count;

    public int EntryCount => map.Values.Sum(s => s.Count);

    public int Count(TKey key) => map.TryGetValue(key, out var stack) ? stack.Count : 0;

    public bool ContainsKey(TKey key) => map.ContainsKey(key);

    public void Push(TKey key, TValue value)
    {
        if (!map.ContainsKey(key))
        {
            map.Add(key, new Stack<TValue>());
        }

        map[key].Push(value);
    }

    public TValue Pop(TKey key) => TryPop(key, out var value) ? value : throw KeyNotFound(key);

    public TValue Peek(TKey key) => TryPeek(key, out var value) ? value : throw KeyNotFound(key);

    public bool TryPop(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        if (map.TryGetValue(key, out var stack))
        {
            return stack.TryPop(out value);
        }

        value = default;
        return false;
    }

    public bool TryPeek(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        if (map.TryGetValue(key, out var stack))
        {
            return stack.TryPeek(out value);
        }

        value = default;
        return false;
    }

    public void Clear() => map.Clear();

    public void Clear(TKey key)
    {
        if (map.ContainsKey(key))
        {
            map[key].Clear();
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => map.SelectMany(e => e.Value, (k, v) => KeyValuePair.Create(k.Key, v)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private Exception KeyNotFound(TKey key) => new KeyNotFoundException($"Key not found in KeyedStack. Key: {key}");
}
