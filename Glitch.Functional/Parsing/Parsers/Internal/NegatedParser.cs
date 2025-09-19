using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class NegatedParser<TToken, T> : Parser<TToken, Unit>
    {
        private readonly Parser<TToken, T> parser;

        public NegatedParser(Parser<TToken, T> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, Unit> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input)
                         .Match(
                             okay: val => ParseResult<TToken, Unit>.Error($"Negated parser succeeded with {val}", input),
                             error: _ => ParseResult.Okay(Unit.Value, input));
        }
    }
}
