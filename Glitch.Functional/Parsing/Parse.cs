using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static TokenParser<TToken> Satisfy<TToken>(Func<TToken, bool> predicate)
        => Satisfy(predicate, Expectation<TToken>.None);

    public static TokenParser<TToken> Satisfy<TToken>(Func<TToken, bool> predicate, Expectation<TToken> expectation)
        => new TokenParser<TToken>(predicate, expectation);

    public static TokenParser<TToken> Any<TToken>() => Satisfy<TToken>(_ => true);

    public static TokenParser<TToken> Token<TToken>(TToken token)
    {
        return Satisfy(t => t?.Equals(token) == true, Expectation.Expected(token));
    }

    public static Parser<TToken, Unit> Not<TToken, T>(Parser<TToken, T> parser)
        => parser.Not();
}
