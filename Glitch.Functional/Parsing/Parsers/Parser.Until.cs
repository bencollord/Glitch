using Glitch.Functional;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    /// <summary>
    /// Repeats the current <see cref="Parser{TToken, T}"/> until <paramref name="stop"/>
    /// succeeds, discarding the result of <paramref name="stop"/>.
    /// </summary>
    /// <typeparam name="TStop"></typeparam>
    /// <param name="stop"></param>
    /// <returns></returns>
    public virtual Parser<TToken, IEnumerable<T>> Until<TStop>(Parser<TToken, TStop> stop)
        => new UntilParser<TToken, T, TStop>(this, stop);
}

internal class UntilParser<TToken, T, TStop> : Parser<TToken, IEnumerable<T>>
{
    private readonly Parser<TToken, T> parser;
    private readonly Parser<TToken, TStop> stop;

    internal UntilParser(Parser<TToken, T> parser, Parser<TToken, TStop> stop)
    {
        this.parser = parser;
        this.stop = stop;
    }

    public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
    {
        var remaining = input;
        var items = new List<T>();

        while (!remaining.IsEnd)
        {
            var stopResult = stop.Execute(remaining);

            if (stopResult.IsOkay)
            {
                remaining = stopResult.Remaining;
                break;
            }

            var result = parser.Execute(remaining);

            items.Add((T)result);
            remaining = result.Remaining;
        }

        return ParseResult.Okay(items.AsEnumerable(), remaining);
    }
}

