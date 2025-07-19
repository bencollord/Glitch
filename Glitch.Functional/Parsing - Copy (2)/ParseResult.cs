namespace Glitch.Functional.Parsing
{
    public static class ParseResult
    {
        public static ParseResult<T> Okay<T>(T value) => Okay(value, string.Empty);

        public static ParseResult<T> Okay<T>(T value, string remaining) => new(Result.Okay<T, ParseError>(value), remaining);

        public static ParseResult<T> Fail<T>(string message) => Fail<T>(new ParseError(message));

        public static ParseResult<T> Fail<T>(ParseError error) => Fail<T>(error, string.Empty);

        public static ParseResult<T> Fail<T>(string message, string remaining) => Fail<T>(new ParseError(message), remaining);

        public static ParseResult<T> Fail<T>(ParseError error, string remaining) => new(Result.Fail<T, ParseError>(error), remaining);
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

        public static implicit operator ParseResult<T>(T value) => new(Okay(value), string.Empty);

        public static implicit operator ParseResult<T>(ParseError error) => new(Fail(error), string.Empty);
    }
}
