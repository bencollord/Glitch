using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal class LookaheadParser<TToken, T> : Parser<TToken, Option<T>>
    {
        private readonly Parser<TToken, T> parser;

        internal LookaheadParser(Parser<TToken, T> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, Option<T>> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input)
                         .Match(ok => ok.Map(Some) with { Remaining = input },
                                err => ParseResult<TToken>.Okay(Option<T>.None, input));
        }
    }
}
