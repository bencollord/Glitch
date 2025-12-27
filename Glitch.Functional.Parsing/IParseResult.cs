using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public interface IParseResult<TToken, out T> : IResult<T, ParseError>
{
    ITokenSequence<TToken> Remaining { get; init; }
}
