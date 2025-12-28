using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Option;

// TODO Decide whether this or the non-generic Parse class should be the canonical source for token parsers.
public static class Parse<TToken>
{
    public static ITokenParser<TToken> Any => Satisfy(_ => true);

    public static ITokenParser<TToken> Satisfy(Func<TToken, bool> predicate) => new TokenParser<TToken>(predicate, None);

    public static ITokenParser<TToken> Satisfy(Func<TToken, bool> predicate, string label) => new TokenParser<TToken>(predicate, Some(label));

    public static ITokenParser<TToken> Token([DisallowNull] TToken token) => Satisfy(t => t!.Equals(token)).WithLabel($"'{token}'");
}
