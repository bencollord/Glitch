using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class LookaheadParser<TToken, T> : IParser<TToken, T>
{
    private readonly IParser<TToken, T> parser;

    public LookaheadParser(IParser<TToken, T> parser)
    {
        this.parser = parser;
    }

    public IParseResult<TToken, T> Execute(ITokenSequence<TToken> input)
    {
        return parser.Execute(input)
                     .Match(ok => ParseResult<TToken>.Okay(ok, input), // Backtrack
                            err => ParseResult<TToken>.Error<T>(err, input));
    }
}