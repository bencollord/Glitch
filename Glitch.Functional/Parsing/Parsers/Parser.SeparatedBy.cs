namespace Glitch.Functional.Parsing
{
    using static Parse;

    public abstract partial class Parser<TToken, T>
    {
        public virtual SeparatedByContext<TToken, T, TToken> SeparatedBy(TToken token)
            => SeparatedBy(Token(token));

        public virtual SeparatedByContext<TToken, T, TSeparator> SeparatedBy<TSeparator>(Parser<TToken, TSeparator> separator)
            => new(this, separator);
    }

    public class SeparatedByContext<TToken, T, TSeparator>
    {
        private Parser<TToken, T> parser;
        private Parser<TToken, TSeparator> separator;

        internal SeparatedByContext(Parser<TToken, T> parser, Parser<TToken, TSeparator> separator)
        {
            this.parser = parser;
            this.separator = separator;
        }

        public Parser<TToken, IEnumerable<T>> AtLeastOnce() => from once in parser
                                                               from rest in separator
                                                                   .Then(parser)
                                                                   .ZeroOrMoreTimes()
                                                               let items = rest.Prepend(once)
                                                               from last in parser.Maybe()
                                                               select last.Select(items.Append)
                                                                          .IfNone(items);

        
        public Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes()
            // TODO Add support for allowing/disallowing a terminating separator
            => parser.Before(separator)
                     .ZeroOrMoreTimes()
                     .Then(parser.Maybe(), (items, lastOpt) => 
                         lastOpt.Match(
                             some: items.Append,
                             none: items));

        public Parser<TToken, IEnumerable<T>> Times(int count)
            => from once in parser
               from rest in separator.Then(parser).Times(count - 1)
               select rest.Prepend(once);

        public static implicit operator Parser<TToken, IEnumerable<T>>(SeparatedByContext<TToken, T, TSeparator> context) => context.ZeroOrMoreTimes();
    }
}
