using Glitch.Functional.Parsing.Legacy.Input;

namespace Glitch.Functional.Parsing.Legacy.Results;

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
