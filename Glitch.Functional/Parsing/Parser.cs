using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        private Func<TokenSequence<TToken>, ParseResult<TToken, T>> parser;

        internal Parser(Func<TokenSequence<TToken>, ParseResult<TToken, T>> parser)
        {
            this.parser = parser;
        }

        public Parser<TToken, TResult> Map<TResult>(Func<T, TResult> selector)
            => new(input => parser(input).Map(selector));

        public Parser<TToken, T> Filter(Func<T, bool> predicate)
           => new(input => parser(input).Filter(predicate));

        public Parser<TToken, T> Guard(Func<T, bool> predicate, Func<T, Expectation<TToken>> ifFail)
            => new(input => parser(input).Guard(predicate, ifFail));

        public Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> ifOkay, Func<ParseError<TToken, T>, TResult> ifFail)
            => new(input => parser(input).Match(ifOkay, ifFail));

        public Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> ifOkay, Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> ifFail)
            => new(input => parser(input).Match(ifOkay, ifFail));

        public Parser<TToken, T> WithRemaining(TokenSequence<TToken> remaining)
            => Match(ok => ok with { Remaining = remaining },
                     err => err with { Remaining = remaining });

        public static Parser<TToken, T> operator |(Parser<TToken, T> x, Parser<TToken, T> y) => x.Or(y);

        public static Parser<TToken, IEnumerable<T>> operator +(Parser<TToken, T> x, Parser<TToken, T> y) => x.Then<T, IEnumerable<T>>(y, (a, b) => [a, b]);

        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Parser<TToken, T> y) => x.Then(y);
        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Parser<TToken, Unit> y) => x.Then(v => y.Map(_ => v));

        public static implicit operator Parser<TToken, T>(T value) => Return(value);

        public static implicit operator Parser<TToken, T>(ParseResult<TToken, T> result) => new(_ => result);

        public static implicit operator Parser<TToken, T>(Func<TokenSequence<TToken>, ParseResult<TToken, T>> parser) => new(parser);
    }
}
