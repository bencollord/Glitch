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

        public Parser<TToken, T> Guard(Func<T, bool> predicate, Func<T, ParseError<TToken>> ifFail)
            => new(input => parser(input).Guard(predicate, ifFail));

        public Parser<TToken, TResult> Match<TResult>(Func<T, TResult> ifOkay, Func<ParseError<TToken>, TResult> ifFail)
            => new(input => parser(input).Match(ifOkay, ifFail));

        public ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
            => parser(input);

        public static implicit operator Parser<TToken, T>(T value) => Return(value);

        public static implicit operator Parser<TToken, T>(ParseError<TToken> error) => Error(error);

        public static implicit operator Parser<TToken, T>(ParseResult<TToken, T> result) => new(_ => result);

        public static implicit operator Parser<TToken, T>(Func<TokenSequence<TToken>, ParseResult<TToken, T>> parser) => new(parser);
    }
}
