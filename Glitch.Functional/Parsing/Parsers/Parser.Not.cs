using Glitch.Functional;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, Unit> Not()
        => new NegatedParser<TToken, T>(this);
}

internal class NegatedParser<TToken, T> : Parser<TToken, Unit>
{
    private readonly Parser<TToken, T> parser;

    public NegatedParser(Parser<TToken, T> parser)
    {
        this.parser = parser;
    }

    public override ParseResult<TToken, Unit> Execute(TokenSequence<TToken> input)
    {
        return parser.Execute(input)
                     .Match(
                         okay: val => ParseResult<TToken, Unit>.Error($"Negated parser succeeded with {val}", input),
                         fail: _ => ParseResult.Okay(Unit.Value, input));
    }
}
