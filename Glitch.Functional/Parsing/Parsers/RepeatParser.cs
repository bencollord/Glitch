using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using Glitch.Functional.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class RepeatParser<TToken, T> : Parser<TToken, IEnumerable<T>>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Option<int> min;
        private readonly Option<int> max;

        internal RepeatParser(Parser<TToken, T> parser, int? min = null, int? max = null)
        {
            this.parser = parser;
            this.min = Option.Maybe(min);
            this.max = Option.Maybe(max);
        }

        public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
        {
            var remaining = input;
            var items = new List<T>();
            int count = 0;

            while (!remaining.IsEnd)
            {
                var result = parser.Execute(remaining);

                if (!result.IsOkay)
                {
                    break;
                }
                
                count++;
                
                if (max.IsSomeAnd(m => count > m))
                {
                    return ParseResult<TToken>.Error<IEnumerable<T>>($"Expected no more than {max.Unwrap()} items, found {count}", input);
                }

                items.Add((T)result);
                remaining = result.Remaining;
            }

            if (min.IsSomeAnd(m => count < m))
            {
                return ParseResult<TToken>.Error<IEnumerable<T>>($"Expected {count} times, found only {items.Count} times", remaining);
            }

            return ParseResult.Okay(items.AsEnumerable(), remaining);
        }
    }
}
