using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static Parser<TToken, ParseState<TToken>> State<TToken, T>(Parser<TToken, T> parser) => new StateParser<TToken, T>(parser);

    /// <summary>
    /// A parser that returns the current <see cref="ParseState{TToken}"/> without consuming input.
    /// </summary>
    /// <typeparam name="TToken"></typeparam>
    /// <typeparam name="T"></typeparam>
    private class StateParser<TToken, T> : Parser<TToken, ParseState<TToken>>
    {
        private Parser<TToken, T> parser;

        internal StateParser(Parser<TToken, T> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, ParseState<TToken>> Execute(TokenSequence<TToken> input)
        {
            var result = parser.Lookahead().Execute(input);

            return ParseResult.Okay<TToken, ParseState<TToken>>(new(result.Expectation, input));
        }
    }
}
