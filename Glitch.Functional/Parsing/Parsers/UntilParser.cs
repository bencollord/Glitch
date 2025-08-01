using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class UntilParser<TToken, T, TOther> : Parser<TToken, IEnumerable<T>>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Parser<TToken, TOther> other;

        internal UntilParser(Parser<TToken, T> parser, Parser<TToken, TOther> other)
        {
            this.parser = parser;
            this.other = other;
        }

        // TODO Remove duplication between this and SequenceParser
        public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
        {
            // TODO Remove duplication with ZeroOrMoreTimes and replace with internal iterator
            var remaining = input;
            var items = new List<T>();

            while (!other.Execute(remaining) && !remaining.IsEnd)
            {
                var result = parser.Execute(remaining);

                if (!result.WasSuccessful)
                {
                    return result.Cast<IEnumerable<T>>();
                }

                items.Add((T)result);
                remaining = result.Remaining;
            }

            return ParseResult.Okay(items.AsEnumerable(), remaining);
        }
    }
}
