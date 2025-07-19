namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        public Parser<TToken, TOther> Then<TOther>(Parser<TToken, TOther> other)
            => Then(_ => other);

        public Parser<TToken, TResult> Then<TResult>(Func<T, Parser<TToken, TResult>> selector)
            => new(input => parser(input).AndThen(output => selector(output).parser(input)));

        public Parser<TToken, TResult> Then<TElement, TResult>(Func<T, Parser<TToken, TElement>> selector, Func<T, TElement, TResult> projection)
            => new(input => parser(input).AndThen(output => selector(output).parser(input), projection));

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

        public Parser<TToken, Option<T>> Maybe() => Match(Some, _ => None);

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
                return ParseResult.Fail<TToken, T>("Expected end of input");
            }

            return result;
        });
    }
}
