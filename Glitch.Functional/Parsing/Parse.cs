using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<TToken, TToken> Satisfy<TToken>(Func<TToken, bool> predicate)
            => new TokenParser<TToken>(predicate);

        public static Parser<TToken, TToken> Any<TToken>() => Satisfy<TToken>(_ => true);

        public static Parser<TToken, TToken> Token<TToken>(TToken token)
        {
            return Satisfy<TToken>(t => t?.Equals(token) == true);
        }

        public static Parser<TToken, Unit> Not<TToken, T>(Parser<TToken, T> parser)
            => parser.Not();

        public static Parser<TToken, IEnumerable<TToken>> Sequence<TToken>(IEnumerable<Parser<TToken, TToken>> parsers)
            => new SequenceParser<TToken, TToken>(parsers);
    }
}
