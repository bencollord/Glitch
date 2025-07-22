namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        public Parser<TToken, TOther> Then<TOther>(Parser<TToken, TOther> other)
            => Then(_ => other);

        public Parser<TToken, TResult> Then<TResult>(Func<T, Parser<TToken, TResult>> selector)
            => new(input => parser(input).Match(ok => selector(ok.Value).parser(ok.Remaining),
                                                err => err.Cast<TResult>()));

        public Parser<TToken, TResult> Then<TElement, TResult>(Func<T, Parser<TToken, TElement>> selector, Func<T, TElement, TResult> projection)
            => Then(x => selector(x).Map(y => projection(x, y)));

        public Parser<TToken, T> Or(Parser<TToken, T> other)
            => new(input =>
            {
                var result = parser(input);

                return result.WasSuccessful ? result : other.parser(input);
            });

        public Parser<TToken, Unit> Not() => new(input => 
        {
            var result = parser(input);

            return result.WasSuccessful
                 ? ParseResult.Fail<TToken, Unit>("Negated parser was successful", input)
                 : ParseResult.Okay(Unit.Value, result.Remaining);
        });

        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success and a successful result containing
        /// <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <returns></returns>
        public Parser<TToken, Option<T>> Maybe()
            => new(input => parser(input).Match(ok => ok.Map(Some),
                                                err => ParseResult.Okay(Option<T>.None, input)));

        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success without consuming any input and a successful result
        /// containing <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <remarks>
        /// Like <see cref="Maybe()"/>, but does not advance the input further
        /// </remarks>
        /// <returns></returns>
        public Parser<TToken, Option<T>> Lookahead()
            => new(input => parser(input).Match(ok => ParseResult.Okay(Some(ok.Value), input),
                                                err => ParseResult.Okay(Option<T>.None, input)));

        public Parser<TToken, T> Between<TLeft, TRight>(Parser<TToken, TLeft> left, Parser<TToken, TRight> right)
            => from l in left
               from v in this
               from r in right
               select v;

        public Parser<TToken, T> EndOfInput() => new(input =>
        {
            var result = parser(input);

            if (result.WasSuccessful && !result.Remaining.IsEnd)
            {
                var expectation = new Expectation<TToken>
                {
                    Label = "end of input",
                    Unexpected = result.Remaining.Peek()
                };

                return ParseResult.Fail<TToken, T>("Expected end of input");
            }

            return result;
        });
    }
}
