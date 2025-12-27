namespace Glitch.Functional.Parsing.Results;

public record ParseFailure<TToken, T>(ParseError Error, ITokenSequence<TToken> Remaining) : IParseResult<TToken, T>
{
    public bool IsOkay => false;

    public bool IsFail => true;

    public TResult Match<TResult>(Func<T, TResult> okay, Func<ParseError, TResult> fail) => fail(Error);
}
