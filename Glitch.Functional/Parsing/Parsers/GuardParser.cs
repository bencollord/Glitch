using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal class GuardParser<TToken, T> : Parser<TToken, T>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Func<T, bool> predicate;
        private readonly Func<T, ParseError<TToken, T>> onFail;

        internal GuardParser(Parser<TToken, T> parser, Func<T, bool> predicate, Func<T, ParseError<TToken, T>> onFail)
        {
            this.parser = parser;
            this.predicate = predicate;
            this.onFail = onFail;
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input).AndThen<T>(ok => predicate(ok.Value) ? ok : onFail(ok.Value) with { Remaining = ok.Remaining });
        }
    }
}
