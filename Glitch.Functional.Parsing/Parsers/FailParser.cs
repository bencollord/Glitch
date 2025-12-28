using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

/// <summary>
/// A parser that returns an error without consuming input.
/// </summary>
/// <typeparam name="TToken"></typeparam>
/// <typeparam name="T"></typeparam>
internal class FailParser<TToken, T> : IParser<TToken, T>
{
    private ParseError error;

    internal FailParser(ParseError error)
    {
        this.error = error;
    }

    public IParseResult<TToken, T> Execute(ITokenSequence<TToken> input) => ParseResult.Error<TToken, T>(error, input);
}