using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

/// <summary>
/// A parser that returns a predefined <see cref="IParseResult{TToken, T}">result</see>,
/// replacing any values, errors, or remaining input with the provided result.
/// </summary>
/// <typeparam name="TToken"></typeparam>
/// <typeparam name="T"></typeparam>
internal class ReturnParser<TToken, T> : IParser<TToken, T>
{
    private IParseResult<TToken, T> result;

    public ReturnParser(IParseResult<TToken, T> result)
    {
        this.result = result;
    }

    public IParseResult<TToken, T> Execute(ITokenSequence<TToken> _) => result;
}

