using Glitch.Functional;
using Glitch.Functional.Parsing.Input;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing.Results;

public record ParseSuccess<TToken, T>(T Value, ITokenSequence<TToken> Remaining) : IParseResult<TToken, T>
{
    public bool IsOkay => true;

    public bool IsFail => false;

    public TResult Match<TResult>(Func<T, TResult> okay, Func<ParseError, TResult> fail) => okay(Value);
}
