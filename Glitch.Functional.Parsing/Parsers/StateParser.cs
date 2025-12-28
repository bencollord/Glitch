using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

/// <summary>
/// A parser that returns the current <see cref="ParseState{TToken}"/> without consuming input.
/// </summary>
/// <typeparam name="TToken"></typeparam>
/// <typeparam name="T"></typeparam>
internal class StateParser<TToken> : IParser<TToken, ParseState<TToken>>
{
    public IParseResult<TToken, ParseState<TToken>> Execute(ITokenSequence<TToken> input) => ParseResult.Okay(new ParseState<TToken>(input), input);
}

