using Glitch.Functional;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class UntilParser<TToken, T, TStop> : IParser<TToken, IEnumerable<T>>
{
    private readonly IParser<TToken, T> parser;
    private readonly IParser<TToken, TStop> stop;

    internal UntilParser(IParser<TToken, T> parser, IParser<TToken, TStop> stop)
    {
        this.parser = parser;
        this.stop = stop;
    }

    public IParseResult<TToken, IEnumerable<T>> Execute(ITokenSequence<TToken> input)
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

            if (result.IsFail)
            {
                return result.Select(DynamicCast<IEnumerable<T>>.From); // Too lazy to add a Cast method to this.
            }

            items.Add(result.Unwrap());
            remaining = result.Remaining;
        }

        return ParseResult.Okay(items.AsEnumerable(), remaining);
    }
}
