using Glitch.Functional.Parsing.Extensions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class NegatedParser<TToken, T> : Parser<TToken, Nothing>
    {
        private readonly Parser<TToken, T> parser;

        public NegatedParser(Parser<TToken, T> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, Nothing> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input)
                         .Match(
                             okay: val => ParseResult<TToken, Nothing>.Error($"Negated parser succeeded with {val}", input),
                             error: _ => ParseResult.Okay(Nothing.Value, input));
        }
    }
}
