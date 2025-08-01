using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal class TokenParser<TToken> : Parser<TToken, TToken>
    {
        private Func<TToken, bool> predicate;

        internal TokenParser(Func<TToken, bool> predicate)
        {
            this.predicate = predicate;
        }

        public override ParseResult<TToken, TToken> Execute(TokenSequence<TToken> input)
        {
            if (input.IsEnd)
                return ParseResult.Error<TToken, TToken>("End of input reached");

            if (predicate(input.Current))
            {
                return ParseResult.Okay(input.Current, input.Advance());
            }

            return ParseResult<TToken>.Error<TToken>(Expectation.Unexpected(input.Current), input);
        }
    }
}
