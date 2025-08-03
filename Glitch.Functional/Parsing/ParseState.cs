using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Results
{
    public record ParseState<TToken>
    {
        internal ParseState(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
        {
            Remaining = remaining;
            Expectation = expectation;
        }

        public TokenSequence<TToken> Remaining { get; init; }

        public Expectation<TToken> Expectation { get; init; }
    }
}
