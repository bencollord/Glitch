namespace Glitch.Functional.Parsing
{
    public static class ParseResult
    {
        public static ParseResult<T> Okay<T>(T value) => new(Result<T, ParseError>.Okay(value), string.Empty);

        public static ParseResult<T> Fail<T>(ParseError error) => new(Result<T, ParseError>.Fail(error), string.Empty);
    }

    public record ParseResult<T>
    {
        private Result<T, ParseError> result;

        internal ParseResult(Result<T, ParseError> result, string remaining)
        {
            this.result = result;

            Remaining = remaining;
        }

        public string Remaining { get; }

        public ParseResult<TResult> Map<TResult>(Func<T, TResult> map) => new(result.Map(map), Remaining);

        public ParseResult<TResult> AndThen<TResult>(Func<T, ParseResult<TResult>> bind) => result.Match(bind, ParseResult.Fail<TResult>);

        public ParseResult<TResult> AndThen<TElement, TResult>(Func<T, ParseResult<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<ParseError, TResult> ifFail)
            => result.Match(ifOkay, ifFail);

        public static implicit operator ParseResult<T>(Success<T> success) => new(Result<T, ParseError>.Okay(success.Value), string.Empty);

        public static implicit operator ParseResult<T>(Failure<ParseError> failure) => new(Result<T, ParseError>.Fail(failure.Error), string.Empty);
    }
}
