using System.Collections.Immutable;

namespace Glitch.Functional.Parsing.Results;

public record Expectation
{
    public static readonly Expectation None = new([], Option.None, Option.None);

    private ImmutableArray<string> expectations;
    private Option<int> minCount;
    private Option<int> maxCount;

    public Expectation(ImmutableArray<string> expectations, Option<int> minCount = default, Option<int> max = default)
    {
        this.expectations = expectations;
        this.minCount = minCount;
        this.maxCount = max;
    }

    public int Count => expectations.Length;

    public Expectation Times(int count) => Times(count, count);

    public Expectation Times(Option<int> min, Option<int> max) => new(expectations, min, max);

    public Expectation Merge(Expectation other)
    {
        // DESIGN The more I think about this, the more I feel like expectations of specific parsers
        // and expectations of a certain -count- of successful parsers is a separate concern that warrants
        // something like the decorator pattern, but I'm not in the mood to deal with that right now.
        if (minCount == other.minCount && maxCount == other.maxCount)
        {
            return new(expectations.Concat(other.expectations).ToImmutableArray(), minCount, maxCount);
        }

        return new([ToString(), other.ToString()]);
    }

    public override string ToString() => FormatExpectations() + FormatCount().Match(some: x => ' ' + x, none: string.Empty);

    public static Expectation operator +(Expectation x, Expectation y) => x.Merge(y);

    private Option<string> FormatCount()
    {
        var atLeast = minCount.Select(x => $"at least {x} times");
        var atMost = maxCount.Select(x => $"no more than {x} times");
        var both = atLeast.Zip(atMost).Select(x => $"{x.Left} but {x.Right}");
        var exactly = from x in minCount
                      from y in maxCount
                      where x == y
                      select $"exactly {x} times";

        return (atLeast | atMost) ^ both ^ exactly; // UNDONE Test this
    }

    private string FormatExpectations()
    {
        return expectations switch
        {
            [var single] => single,
            [var one, var two] => $"{one} or {two}",
            [.. var items, var last] => $"{items.Join(", ")}, or {last}",
            [] => string.Empty
        };
    }
}
