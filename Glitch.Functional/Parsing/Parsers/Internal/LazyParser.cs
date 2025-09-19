using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class LazyParser<TToken, T> : Parser<TToken, T>
    {
        private Lazy<Parser<TToken, T>> parser;

        internal LazyParser(Lazy<Parser<TToken, T>> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input) => parser.Value.Execute(input);
    }
}
