using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class BindParser<TToken, TSource, TNext> : IParser<TToken, TNext>
{
    private readonly IParser<TToken, TSource> source;
    private readonly Func<TSource, IParser<TToken, TNext>> next;

    internal BindParser(IParser<TToken, TSource> source, Func<TSource, IParser<TToken, TNext>> next)
    {
        this.source = source;
        this.next = next;
    }

    public IParseResult<TToken, TNext> Execute(ITokenSequence<TToken> input)
    {
        var result = source.Execute(input);

        return result.Match(
            okay => next(okay).Execute(result.Remaining),
            err => ParseResult<TToken>.Error<TNext>(err, input));
    }
}
