using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class SliceParser<TToken, T, TResult> : Parser<TToken, TResult>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Func<ReadOnlySpan<TToken>, T, TResult> slice;

        internal SliceParser(Parser<TToken, T> parser, Func<ReadOnlySpan<TToken>, T, TResult> slice)
        {
            this.parser = parser;
            this.slice = slice;
        }

        public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
        {
            var parseResult = parser.Execute(input);

            if (parseResult is ParseSuccess<TToken, T> ok)
            {
                var delta = parseResult.Remaining.Position - input.Position;
                var consumed = parseResult.Remaining.Lookback(delta);
                var result = slice(consumed, ok.Value);

                return ParseResult<TToken, TResult>.Okay(result, parseResult.Remaining);
            }

            return parseResult.Cast<TResult>();
        }
    }
}