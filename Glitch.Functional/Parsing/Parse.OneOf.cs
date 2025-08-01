using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<TToken, TToken> OneOf<TToken>(params IEnumerable<TToken> tokens)
            => OneOf(tokens.Select(Token));

        public static Parser<TToken, T> OneOf<TToken, T>(params IEnumerable<Parser<TToken, T>> parsers)
            => new OneOfParser<TToken, T>(parsers);
    }
}
