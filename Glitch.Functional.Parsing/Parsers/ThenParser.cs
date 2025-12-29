using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class ThenParser<TToken, TSource, TNext> : IParser<TToken, TNext>
{
    private readonly IParser<TToken, TSource> source;
    private readonly Func<TSource, IParser<TToken, TNext>> next;

    internal ThenParser(IParser<TToken, TSource> source, Func<TSource, IParser<TToken, TNext>> next)
    {
        this.source = source;
        this.next = next;
    }

    public IParseResult<TToken, TNext> Execute(ITokenSequence<TToken> input)
    {
        var result = source.Execute(input);

        var nextResult = result.Match(
            okay => next(okay).Execute(result.Remaining),
            err => ParseResult<TToken>.Error<TNext>(err, input));

        return nextResult;
    }
}
