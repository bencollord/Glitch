using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static IParser<TToken, ParseState<TToken>> State<TToken>() => ParseState<TToken>.Get;

    public static IParser<TToken, T> Return<TToken, T>(T value) => State<TToken>().Then(s => Return(ParseResult.Okay(value, s)));

    public static IParser<TToken, T> Return<TToken, T>(IParseResult<TToken, T> result) => new ReturnParser<TToken, T>(result);

    public static ITokenParser<TToken> OneOf<TToken>(params IEnumerable<TToken> tokens) => Satisfy<TToken>(t => tokens.Contains(t));

    public static IParser<TToken, TToken> OneOf<TToken>(params IEnumerable<ITokenParser<TToken>> parsers) => new OneOfParser<TToken, TToken>(parsers);

    public static IParser<TToken, T> OneOf<TToken, T>(params IEnumerable<IParser<TToken, T>> parsers) => new OneOfParser<TToken, T>(parsers);

    public static IParser<TToken, IEnumerable<TToken>> Sequence<TToken>(IEnumerable<IParser<TToken, TToken>> parsers) => new SequenceParser<TToken, TToken, IEnumerable<TToken>>(parsers, FN.Identity);
}
