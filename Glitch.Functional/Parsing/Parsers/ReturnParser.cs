using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    /// <summary>
    /// Returns a provided <see cref="ParseResult{TToken, T}"/> without consuming any input.
    /// </summary>
    /// <typeparam name="TToken"></typeparam>
    /// <typeparam name="T"></typeparam>
    internal class ReturnParser<TToken, T> : Parser<TToken, T>
    {
        private readonly Result<T, Expectation<TToken>> result;
        private readonly Option<TokenSequence<TToken>> remainingOverride = Option.None;

        internal ReturnParser(T value)
        {
            result = Result.Okay<T, Expectation<TToken>>(value);
        }

        internal ReturnParser(T value, TokenSequence<TToken> inputOverride)
            : this(value)
        {
            remainingOverride = Option.Some(inputOverride);
        }

        internal ReturnParser(Expectation<TToken> expectation)
        {
            result = Result.Fail<T, Expectation<TToken>>(expectation);
        }

        internal ReturnParser(Expectation<TToken> expectation, TokenSequence<TToken> inputOverride)
            : this(expectation)
        {
            remainingOverride = Option.Some(inputOverride);
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
            => result.Match(
                   okay: v => ParseResult<TToken>.Okay(v, remainingOverride.IfNone(input)),
                   error: e => ParseResult<TToken>.Error<T>(e, remainingOverride.IfNone(input)));
    }
}
