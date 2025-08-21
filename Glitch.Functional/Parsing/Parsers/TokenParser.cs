using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class TokenParser<TToken> : Parser<TToken, TToken>
    {
        private Func<TToken, bool> predicate;
        private Expectation<TToken> expectation;

        internal TokenParser(TToken token)
            : this(t => t!.Equals(token), Expectation.Expected(token)) { }

        internal TokenParser(Func<TToken, bool> predicate, Expectation<TToken> expectation)
        {
            this.predicate = predicate;
            this.expectation = expectation;
        }

        public override ParseResult<TToken, TToken> Execute(TokenSequence<TToken> input)
        {
            return predicate(input.Current)
                 ? ParseResult.Okay(input.Current, input.Advance())
                 : ParseResult.Error<TToken, TToken>(expectation with { Unexpected = input.Current }, input);
        }
    }
}
