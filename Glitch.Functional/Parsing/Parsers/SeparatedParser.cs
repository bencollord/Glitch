using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class SeparatedParser<TToken, T, TSeparator> : Parser<TToken, IEnumerable<T>>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Parser<TToken, TSeparator> separator;

        internal SeparatedParser(Parser<TToken, T> parser, Parser<TToken, TSeparator> separator)
        {
            this.parser = parser;
            this.separator = separator;
        }

        // TODO Remove duplication between this and SequenceParser
        // TODO Allow specification of quantity. This currently has zero-or-more times
        //      semantics and always allows a trailing separator.
        public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
        {
            // TODO Remove duplication with ZeroOrMoreTimes and replace with internal iterator
            return parser.Before(separator)
                         .ZeroOrMoreTimes()
                         .Then(parser, (items, last) => items.Append(last))
                         .Execute(input);

        }
    }
}
