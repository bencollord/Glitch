using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    /// <summary>
    /// A parser that returns an error without consuming input.
    /// </summary>
    /// <typeparam name="TToken"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class FailParser<TToken, T> : Parser<TToken, T>
    {
        private ParseError<TToken, T> error;

        internal FailParser(ParseError<TToken, T> error)
        {
            this.error = error;
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input) => error with { Remaining = input };
    }
}
