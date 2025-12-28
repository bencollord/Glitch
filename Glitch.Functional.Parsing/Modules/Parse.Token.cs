using Glitch.Functional.Parsing.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Option;

public partial class Parse<TToken>
{
    public static ITokenParser<TToken> Any => Satisfy(_ => true);

    public static ITokenParser<TToken> Satisfy(Func<TToken, bool> predicate) => new TokenParser<TToken>(predicate, None);

    public static ITokenParser<TToken> Satisfy(Func<TToken, bool> predicate, string label) => new TokenParser<TToken>(predicate, Some(label));

    public static ITokenParser<TToken> Token(TToken token) => Satisfy(t => t!.Equals(token)).WithLabel($"'{token}'");

    public static ITokenParser<TToken> OneOf(params IEnumerable<TToken> tokens) => Satisfy(t => tokens.Contains(t));
}
