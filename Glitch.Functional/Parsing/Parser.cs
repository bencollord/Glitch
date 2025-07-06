namespace Glitch.Functional.Parsing
{

    public partial class Parser<T>
    {
        private Func<string, ParseResult<T>> parser;

        internal Parser(Func<string, ParseResult<T>> parser)
        {
            this.parser = parser;
        }

        public Parser<TResult> Map<TResult>(Func<T, TResult> selector)
            => new(input => parser(input).Map(selector));

        public Parser<TResult> AndThen<TResult>(Func<T, Parser<TResult>> selector)
            => new(input => parser(input).AndThen(output => selector(output).parser(input)));

        public Parser<TResult> AndThen<TElement, TResult>(Func<T, Parser<TElement>> selector, Func<T, TElement, TResult> projection)
            => new(input => parser(input).AndThen(output => selector(output).parser(input), projection));

        public ParseResult<T> Parse(string input)
            => parser(input);
    }
}
