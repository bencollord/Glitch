using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class SequenceParser<TToken, T, TCollection> : IParser<TToken, TCollection>
    where TCollection : IEnumerable<TToken>
{
    private readonly IEnumerable<IParser<TToken, T>> parsers;
    private readonly Func<IEnumerable<T>, TCollection> collector;

    internal SequenceParser(IEnumerable<IParser<TToken, T>> parsers, Func<IEnumerable<T>, TCollection> collector)
    {
        this.parsers = parsers;
        this.collector = collector;
    }

    public IParseResult<TToken, TCollection> Execute(ITokenSequence<TToken> input)
    {
        var remaining = input;
        var results = new List<T>();

        foreach (var p in parsers)
        {
            var r = p.Execute(remaining);

            if (r.IsFail(out var error))
            {
                return ParseResult<TToken>.Error<TCollection>(error, input); // Backtrack on failure.
            }

            results.Add(r.Unwrap());
            remaining = r.Remaining;
        }

        return ParseResult.Okay(collector(results), remaining);
    }
}
