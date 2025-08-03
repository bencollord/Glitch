using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    /// <summary>
    /// A parser that returns a result without consuming input.
    /// </summary>
    /// <typeparam name="TToken"></typeparam>
    /// <typeparam name="T"></typeparam>
    internal class ReturnParser<TToken, T> : Parser<TToken, T>
    {
        private ParseResult<TToken, T> result;

        internal ReturnParser(ParseResult<TToken, T> result)
        {
            this.result = result;
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input) => result;
    }
}
