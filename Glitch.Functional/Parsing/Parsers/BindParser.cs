using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal partial class BindParser<TToken, T, TNext> : Parser<TToken, TNext>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Func<T, Parser<TToken, TNext>> next;

        internal BindParser(Parser<TToken, T> parser, Func<T, Parser<TToken, TNext>> next)
        {
            this.parser = parser;
            this.next = next;
        }

        public override ParseResult<TToken, TNext> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input)
                         .AndThen(res => next(res.Value).Execute(res.Remaining));
        }
    }
}
