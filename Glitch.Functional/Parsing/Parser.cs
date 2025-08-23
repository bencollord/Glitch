using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public abstract ParseResult<TToken, T> Execute(TokenSequence<TToken> input);

        public virtual Result<T, ParseError> TryParse(TokenSequence<TToken> input)
            => Execute(input).Match(ok => Result.Okay<T, ParseError>(ok.Value),
                                    err => Result.Fail<T, ParseError>(new ParseError(err.Message, typeof(T)))); // TODO Unify ParseError types

        public virtual T Parse(TokenSequence<TToken> input)
            => Execute(input).Match(ok => ok.Value,
                                    err => throw ParseException.FromError(err));

        public static Parser<TToken, T> operator |(Parser<TToken, T> x, Parser<TToken, T> y) => x.Or(y);

        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Parser<TToken, T> y) => x.Then(y);
        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Parser<TToken, Unit> y) => x.Then(v => y.Map(_ => v));
    }
}
