using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal class MatchParser<TToken, T, TResult> : Parser<TToken, TResult>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> ifOkay;
        private readonly Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> ifFail;

        internal MatchParser(Parser<TToken, T> parser, Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> ifOkay, Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> ifFail)
        {
            this.parser = parser;
            this.ifOkay = ifOkay;
            this.ifFail = ifFail;
        }

        public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input)
                         .Match(ifOkay, ifFail);
        }
    }
}
