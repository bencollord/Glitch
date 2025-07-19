namespace Glitch.Functional.Parsing
{
    public static class ParseResult
    {
        public static ParseResult<TToken, T> Okay<TToken, T>(T value) => Okay<TToken, T>(value, TokenSequence<TToken>.Empty);

        public static ParseResult<TToken, T> Okay<TToken, T>(T value, TokenSequence<TToken> remaining) => new(Result.Okay<T, ParseError<TToken>>(value), remaining);

        public static ParseResult<TToken, T> Fail<TToken, T>(string message) => Fail<TToken, T>(new ParseError<TToken>(message));

        public static ParseResult<TToken, T> Fail<TToken, T>(ParseError<TToken> error) => Fail<TToken, T>(error, TokenSequence<TToken>.Empty);

        public static ParseResult<TToken, T> Fail<TToken, T>(string message, TokenSequence<TToken> remaining) => Fail<TToken, T>(new ParseError<TToken>(message), remaining);

        public static ParseResult<TToken, T> Fail<TToken, T>(ParseError<TToken> error, TokenSequence<TToken> remaining) => new(Result.Fail<T, ParseError<TToken>>(error), remaining);
    }

    public record ParseResult<TToken, T>
    {
        private Result<T, ParseError<TToken>> result;

        internal ParseResult(Result<T, ParseError<TToken>> result, TokenSequence<TToken> remaining)
        {
            this.result = result;

            Remaining = remaining;
        }

        public bool WasSuccessful => result.IsOkay;

        public TokenSequence<TToken> Remaining { get; }

        public static ParseResult<TToken, T> Okay(T value, TokenSequence<TToken> remaining)
            => new(Result<T, ParseError<TToken>>.Okay(value), remaining);

        public static ParseResult<TToken, T> Fail(ParseError<TToken> error, TokenSequence<TToken> remaining)
            => new(Result<T, ParseError<TToken>>.Fail(error), remaining);

        public ParseResult<TToken, TResult> Map<TResult>(Func<T, TResult> map) => new(result.Map(map), Remaining);

        public ParseResult<TToken, T> MapError(Func<ParseError<TToken>, ParseError<TToken>> map) => new(result.MapError(map), Remaining);

        public ParseResult<TToken, TResult> AndThen<TResult>(Func<T, ParseResult<TToken, TResult>> bind) => result.Match(bind, ParseResult.Fail<TToken, TResult>);

        public ParseResult<TToken, TResult> AndThen<TElement, TResult>(Func<T, ParseResult<TToken, TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public ParseResult<TToken, TResult> Cast<TResult>() => new(result.Cast<TResult>(), Remaining);

        public ParseResult<TToken, T> Filter(Func<T, bool> predicate)
            => Guard(predicate, ParseError<TToken>.Empty);

        public ParseResult<TToken, T> Guard(Func<T, bool> predicate, ParseError<TToken> error)
            => Guard(predicate, _ => error);

        public ParseResult<TToken, T> Guard(Func<T, bool> predicate, Func<T, ParseError<TToken>> ifFail)
            => AndThen(v => predicate(v) ? this : Fail(ifFail(v), Remaining));

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<ParseError<TToken>, TResult> ifFail)
            => result.Match(ifOkay, ifFail);

        public static implicit operator ParseResult<TToken, T>(T value) => Okay(value, TokenSequence<TToken>.Empty);

        public static implicit operator ParseResult<TToken, T>(ParseError<TToken> error) => Fail(error, TokenSequence<TToken>.Empty);

        public static explicit operator T(ParseResult<TToken, T> result) => result.result.Unwrap();

        public static explicit operator ParseError<TToken>(ParseResult<TToken, T> result) => result.result.UnwrapError();
    }
}
