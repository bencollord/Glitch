using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal class NegatedParser<TToken, T> : Parser<TToken, Unit>
    {
        private readonly Parser<TToken, T> parser;

        internal NegatedParser(Parser<TToken, T> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, Unit> Execute(TokenSequence<TToken> input)
        {
            var result = parser.Execute(input);

            return result.WasSuccessful
                 ? ParseResult.Error<TToken, Unit>("Negated parser was successful", input)
                 : ParseResult.Okay(Unit.Value, result.Remaining);
        }
    }
}
