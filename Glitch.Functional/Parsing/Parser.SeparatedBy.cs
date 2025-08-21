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

        public Parser<TToken, IEnumerable<T>> AtLeastOnce() => BuildParser(p => p.AtLeastOnce());

        public Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes() => BuildParser(p => p.ZeroOrMoreTimes());

        public Parser<TToken, IEnumerable<T>> Times(int count) => BuildParser(p => p.Times(count));

        public static implicit operator Parser<TToken, IEnumerable<T>>(SeparatedByContext<TToken, T, TSeparator> context) => context.ZeroOrMoreTimes();

        private Parser<TToken, IEnumerable<T>> BuildParser(Func<Parser<TToken, T>, Parser<TToken, IEnumerable<T>>> enumerableParserSelector)
        {
            return from items in parser.Before(separator).PipeInto(enumerableParserSelector)
                   from last in parser.Maybe() // TODO Add support for allowing/disallowing a terminating separator
                   select last.Match(some: x => items.Append(x),
                                     none: _ => items);
        }
    }
}
