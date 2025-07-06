namespace Glitch.Functional.Parsing
{

    public partial class Parser<TIn, TOut>
    {
        private Func<TIn, Result<TOut, ParseError>> parser;

        internal Parser(Func<TIn, Result<TOut, ParseError>> parser)
        {
            this.parser = parser;
        }

        public Parser<TIn, TResult> Map<TResult>(Func<TOut, TResult> selector)
            => new(input => parser(input).Map(selector));

        public Parser<TIn, TResult> AndThen<TResult>(Func<TOut, Parser<TIn, TResult>> selector)
            => new(input => parser(input).AndThen(output => selector(output).parser(input)));

        public Parser<TIn, TResult> AndThen<TElement, TResult>(Func<TOut, Parser<TIn, TElement>> selector, Func<TOut, TElement, TResult> projection)
            => new(input => parser(input).AndThen(output => selector(output).parser(input), projection));

        public Parser<TIn, TOut> Filter(Func<TOut, bool> predicate)
            => new(input => parser(input).Guard(predicate, new ParseError()));

        public Result<TOut, ParseError> TryParse(TIn input)
            => parser(input);

        public TOut Parse(TIn input)
            => parser(input).Unwrap();
    }
}
