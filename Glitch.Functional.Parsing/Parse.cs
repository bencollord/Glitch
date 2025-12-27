using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static ITokenParser<TToken> Any<TToken>() => Parse<TToken>.Any;

    public static ITokenParser<TToken> Satisfy<TToken>(Func<TToken, bool> predicate) => Parse<TToken>.Satisfy(predicate);

    public static ITokenParser<TToken> Satisfy<TToken>(Func<TToken, bool> predicate, string label) => Parse<TToken>.Satisfy(predicate, label);

    public static ITokenParser<TToken> Token<TToken>([DisallowNull] TToken token) => Parse<TToken>.Token(token);

    // UNDONE
    //public static Parser<TToken, Unit> Not<TToken, T>(Parser<TToken, T> parser)
    //    => parser.Not();
}
