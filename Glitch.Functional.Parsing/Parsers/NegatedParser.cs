using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class NegatedParser<TToken, T> : IParser<TToken, Unit>
{
    private readonly IParser<TToken, T> parser;

    public NegatedParser(IParser<TToken, T> parser)
    {
        this.parser = parser;
    }

    public IParseResult<TToken, Unit> Execute(ITokenSequence<TToken> input)
    {
        return parser.Execute(input)
                        .Match(okay: val => ParseResult.Error<TToken, Unit>($"Negated parser succeeded with {val}", input),
                            fail: _   => ParseResult.Okay(Unit.Value, input));
    }
}
