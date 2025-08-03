using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class LookaheadParser<TToken, T> : Parser<TToken, Option<T>>
    {
        private readonly Parser<TToken, T> parser;

        public LookaheadParser(Parser<TToken, T> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, Option<T>> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input)
                         .Match(ok => ParseResult.Okay(Some(ok.Value), input), // Backtrack
                                err => ParseResult.Okay(Option<T>.None, input));
        }
    }
}
