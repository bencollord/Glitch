using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    public IParser<TToken, IEnumerable<T>> Once() => Times(1);

    public IParser<TToken, IEnumerable<T>> AtLeastOnce() => AtLeast(1);

    public IParser<TToken, IEnumerable<T>> AtLeast(int times) => new RepeatParser<TToken, T>(this, min: times);

    public IParser<TToken, IEnumerable<T>> ZeroOrMoreTimes() => new RepeatParser<TToken, T>(this);

    public IParser<TToken, IEnumerable<T>> Times(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return new RepeatParser<TToken, T>(this, count, count);
    }
}

