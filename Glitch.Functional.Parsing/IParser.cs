using Glitch.Functional.Errors;

namespace Glitch.Functional.Parsing;

public interface IParser<TToken, out T>
{
    IParseResult<TToken, T> Execute(ITokenSequence<TToken> input);
}
