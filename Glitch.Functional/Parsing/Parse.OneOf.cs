using Glitch.Functional.Parsing.Parsers;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static TokenParser<TToken> OneOf<TToken>(params IEnumerable<TToken> tokens)
            => Satisfy<TToken>(t => tokens.Contains(t));

        public static Parser<TToken, T> OneOf<TToken, T>(params IEnumerable<Parser<TToken, T>> parsers)
            => new OneOfParser<TToken, T>(parsers);
    }
}
