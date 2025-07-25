using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Results
{
    public record EmptyParseResult<TToken, T> : ParseError<TToken, T>
    {
        public EmptyParseResult()
            : this(TokenSequence<TToken>.Empty) { }

        public EmptyParseResult(TokenSequence<TToken> remaining) 
            : base("Nothing was parsed", Expectation<TToken>.None, remaining)
        {
        }
    }
}
