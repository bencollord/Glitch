using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Option;

public static partial class Parse
{
    public static ITokenParser<TToken> Any<TToken>() => Satisfy<TToken>(_ => true);

    public static ITokenParser<TToken> Satisfy<TToken>(Func<TToken, bool> predicate) => new TokenParser<TToken>(predicate, None);

    public static ITokenParser<TToken> Satisfy<TToken>(Func<TToken, bool> predicate, string label) => new TokenParser<TToken>(predicate, Some(label));

    public static ITokenParser<TToken> Token<TToken>(TToken token) => Satisfy<TToken>(t => t!.Equals(token)).WithLabel($"'{token}'");

    public static IParser<TToken, Unit> Not<TToken, T>(IParser<TToken, T> parser) => parser.Not();
}