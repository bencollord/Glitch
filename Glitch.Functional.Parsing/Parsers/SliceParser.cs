using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class SliceParser<TToken, T, TResult> : IParser<TToken, TResult>
{
    private readonly IParser<TToken, T> parser;
    private readonly Func<ReadOnlySpan<TToken>, T, TResult> slice;

    internal SliceParser(IParser<TToken, T> parser, Func<ReadOnlySpan<TToken>, T, TResult> slice)
    {
        this.parser = parser;
        this.slice = slice;
    }

    public IParseResult<TToken, TResult> Execute(ITokenSequence<TToken> input)
    {
        var parseResult = parser.Execute(input);

        return parseResult.Select(val => slice(GetConsumed(), val));

        // Lazy so we don't run this on failure.
        ReadOnlySpan<TToken> GetConsumed() => parseResult.Remaining.Lookback(parseResult.Remaining.Position - input.Position);
    }
}