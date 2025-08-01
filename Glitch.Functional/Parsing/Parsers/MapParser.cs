using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal class MapParser<TToken, T, TResult> : Parser<TToken, TResult>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Func<T, TResult> selector;

        internal MapParser(Parser<TToken, T> parser, Func<T, TResult> selector)
        {
            this.parser = parser;
            this.selector = selector;
        }

        public override Parser<TToken, TNewResult> Map<TNewResult>(Func<TResult, TNewResult> selector)
            => new MapParser<TToken, T, TNewResult>(parser, this.selector.Then(selector));

        public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input).Map(selector);
        }
    }
}
