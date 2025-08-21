using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class RepeatParser<TToken, T> : Parser<TToken, IEnumerable<T>>
    {
        private readonly Parser<TToken, T> parser;

        public RepeatParser(Parser<TToken, T> parser)
        {
            this.parser = parser;
        }

        public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
        {
            var remaining = input;
            var items = new List<T>();

            while (!remaining.IsEnd)
            {
                var result = parser.Execute(remaining);

                if (!result.IsOkay)
                {
                    break;
                }

                items.Add((T)result);
                remaining = result.Remaining;
            }

            return ParseResult.Okay(items.AsEnumerable(), remaining);
        }
    }
}
