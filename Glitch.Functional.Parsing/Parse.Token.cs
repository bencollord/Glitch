using Glitch.Functional.Parsing.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Option;

// TODO Decide whether this or the non-generic Parse class should be the canonical source for token parsers.
public static partial class Parse<TToken>
{
    public static ITokenParser<TToken> Any => Satisfy(_ => true);

    public static IParser<TToken, ParseState<TToken>> State => ParseState<TToken>.Get;

    public static IParser<TToken, T> Return<T>(T value) => State.Then(s => Return(ParseResult.Okay(value, s)));

    public static IParser<TToken, T> Return<T>(IParseResult<TToken, T> result) => new ReturnParser<TToken, T>(result);

    public static ITokenParser<TToken> Satisfy(Func<TToken, bool> predicate) => new TokenParser<TToken>(predicate, None);

    public static ITokenParser<TToken> Satisfy(Func<TToken, bool> predicate, string label) => new TokenParser<TToken>(predicate, Some(label));

    public static ITokenParser<TToken> Token([DisallowNull] TToken token) => Satisfy(t => t!.Equals(token)).WithLabel($"'{token}'");
}
