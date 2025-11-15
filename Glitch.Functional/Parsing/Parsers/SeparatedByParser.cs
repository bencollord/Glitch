using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public class SeparatedByParser<TToken, T, TSeparator> : Parser<TToken, IEnumerable<T>>
    {
        private Parser<TToken, T> parser;
        private Parser<TToken, TSeparator> separator;

        internal SeparatedByParser(Parser<TToken, T> parser, Parser<TToken, TSeparator> separator)
        {
            this.parser = parser;
            this.separator = separator;
        }

        // TODO In the unlikely event that someone tries to call these after
        // calling another method, this will return Parser<IEnumerable<IEnumerable<T>>
        // Need to figure out how I handled this last time.
        public new Parser<TToken, IEnumerable<T>> AtLeastOnce()
            => from once in parser
               from rest in separator
                   .Then(parser)
                   .ZeroOrMoreTimes()
               let items = rest.Prepend(once)
               from last in parser.Maybe()
               select last.Select(items.Append)
                          .IfNone(items);

        
        public new Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes()
            // TODO Add support for allowing/disallowing a terminating separator
            => parser.Before(separator)
                     .ZeroOrMoreTimes()
                     .Then(parser.Maybe(), (items, lastOpt) => 
                         lastOpt.Match(
                             some: items.Append,
                             none: items));

        public new Parser<TToken, IEnumerable<T>> Times(int count)
            => from once in parser
               from rest in separator.Then(parser).Times(count - 1)
               select rest.Prepend(once);

        public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
        {
            return ZeroOrMoreTimes().Execute(input); // Default
        }
    }
}
