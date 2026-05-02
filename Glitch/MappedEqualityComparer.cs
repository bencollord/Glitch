using System.Diagnostics.CodeAnalysis;

namespace Glitch;

public static class MappedEqualityComparer<TSource>
{
    public static IEqualityComparer<TSource> Create<TKey>(Func<TSource, TKey> selector, IEqualityComparer<TKey> comparer) =>
        new MappedEqualityComparer<TSource, TKey>(comparer, selector);
}

public class MappedEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
{
    private readonly IEqualityComparer<TKey> keyComparer;
    private readonly Func<TSource, TKey> selector;

    public MappedEqualityComparer(IEqualityComparer<TKey> keyComparer, Func<TSource, TKey> selector)
    {
        this.keyComparer = keyComparer;
        this.selector = selector;
    }

    public bool Equals(TSource? x, TSource? y) => keyComparer.Equals(Map(x), Map(y));

    public int GetHashCode([DisallowNull] TSource obj) => keyComparer.GetHashCode(selector(obj)!); // TODO Figure out why even the non null type of TKey triggers a nullable warning.

    private TKey? Map(TSource? source) => source is null ? default : selector(source);
}
