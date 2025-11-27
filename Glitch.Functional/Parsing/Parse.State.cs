using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static Parser<TToken, TokenSequence<TToken>> State<TToken>() => new StateParser<TToken>();

    /// <summary>
    /// A parser that returns the current <see cref="ParseState{TToken}"/> without consuming input.
    /// </summary>
    /// <typeparam name="TToken"></typeparam>
    /// <typeparam name="T"></typeparam>
    private class StateParser<TToken> : Parser<TToken, TokenSequence<TToken>>
    {
        public override ParseResult<TToken, TokenSequence<TToken>> Execute(TokenSequence<TToken> input)
        {
            return ParseResult.Okay(input, input);
        }
    }
}
