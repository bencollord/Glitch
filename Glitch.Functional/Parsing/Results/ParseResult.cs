using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Results
{
    public static class ParseResult
    {
        public static ParseResult<TToken, T> Empty<TToken, T>() => ParseResult<TToken>.Empty<T>();

        public static ParseResult<TToken, T> Empty<TToken, T>(TokenSequence<TToken> remaining) => ParseResult<TToken>.Empty<T>(remaining);

        public static ParseResult<TToken, T> Okay<TToken, T>(T value) => ParseResult<TToken>.Okay(value);

        public static ParseResult<TToken, T> Okay<TToken, T>(T value, TokenSequence<TToken> remaining) => ParseResult<TToken>.Okay(value, remaining);

        public static ParseResult<TToken, T> Error<TToken, T>(string message) => ParseResult<TToken>.Error<T>(message);

        public static ParseResult<TToken, T> Error<TToken, T>(string message, TokenSequence<TToken> remaining) => ParseResult<TToken>.Error<T>(message, remaining);

        public static ParseResult<TToken, T> Error<TToken, T>(Expectation<TToken> expectation) => ParseResult<TToken>.Error<T>(expectation);

        public static ParseResult<TToken, T> Error<TToken, T>(Expectation<TToken> expectation, TokenSequence<TToken> remaining) => ParseResult<TToken>.Error<T>(expectation, remaining);
    }

    public static class ParseResult<TToken>
    {
        public static ParseResult<TToken, T> Empty<T>() => new EmptyParseResult<TToken, T>();

        public static ParseResult<TToken, T> Empty<T>(TokenSequence<TToken> remaining) => new EmptyParseResult<TToken, T>(remaining);

        public static ParseResult<TToken, T> Okay<T>(T value) => Okay(value, TokenSequence<TToken>.Empty);

        public static ParseResult<TToken, T> Okay<T>(T value, TokenSequence<TToken> remaining) => new ParseSuccess<TToken, T>(value, remaining);

        public static ParseResult<TToken, T> Error<T>(string message) => Error<T>(message, TokenSequence<TToken>.Empty);

        public static ParseResult<TToken, T> Error<T>(string message, TokenSequence<TToken> remaining) => new ParseError<TToken, T>(message, remaining);

        public static ParseResult<TToken, T> Error<T>(Expectation<TToken> expectation) => Error<T>(expectation, TokenSequence<TToken>.Empty);

        public static ParseResult<TToken, T> Error<T>(Expectation<TToken> expectation, TokenSequence<TToken> remaining) => new ParseError<TToken, T>(expectation, remaining);
    }

    public abstract record ParseResult<TToken, T>
    {
        protected ParseResult(TokenSequence<TToken> remaining)
            : this(remaining, Expectation<TToken>.None) { }

        protected ParseResult(TokenSequence<TToken> remaining, Expectation<TToken> expectation)
        {
            Remaining = remaining;
            Expectation = expectation;
        }

        public abstract bool WasSuccessful { get; }

        public TokenSequence<TToken> Remaining { get; init; }

        public Expectation<TToken> Expectation { get; init; }

        public static ParseResult<TToken, T> Okay(T value, TokenSequence<TToken> remaining)
            => new ParseSuccess<TToken, T>(value, remaining);

        public static ParseResult<TToken, T> Fail(Expectation<TToken> error, TokenSequence<TToken> remaining)
            => new ParseError<TToken, T>(error, remaining);

        public ParseResult<TToken, TResult> Map<TResult>(Func<T, TResult> map) => AndThen(ok => ParseResult<TToken>.Okay(map(ok.Value), ok.Remaining));

        public abstract ParseResult<TToken, TResult> Cast<TResult>();

        public ParseResult<TToken, TResult> AndThen<TResult>(Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> bind)
            => Match(bind, err => err.Cast<TResult>());

        public ParseResult<TToken, TResult> AndThen<TElement, TResult>(Func<ParseSuccess<TToken, T>, ParseResult<TToken, TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x.Value, y)));

        public ParseResult<TToken, T> OrElse(Func<ParseError<TToken, T>, ParseResult<TToken, T>> bind)
            => Match(Identity, bind);
        
        public ParseResult<TToken, TResult> And<TResult>(ParseResult<TToken, TResult> other)
            => WasSuccessful ? other : Cast<TResult>();

        public ParseResult<TToken, T> Or(ParseResult<TToken, T> other) => WasSuccessful ? this : other;

        public ParseResult<TToken, T> XOr(ParseResult<TToken, T> other)
            => (WasSuccessful, other.WasSuccessful) switch
            {
                (true, true) => ParseResult.Error<TToken, T>($"Exclusive OR failed. Both results succeeded. Left {(T)this}, Right: {(T)other}"),
                (true, _) => other,
                (false, _) => this,
            };

        public ParseResult<TToken, T> Filter(Func<T, bool> predicate)
            => Guard(predicate, Expectation<TToken>.None);

        public ParseResult<TToken, T> Guard(Func<T, bool> predicate, Expectation<TToken> error)
            => Guard(predicate, _ => error);

        public ParseResult<TToken, T> Guard(Func<T, bool> predicate, Func<T, Expectation<TToken>> ifFail)
            => AndThen(ok => predicate(ok.Value) ? this : Fail(ifFail(ok.Value), Remaining));

        public abstract TResult Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> ifOkay, Func<ParseError<TToken, T>, TResult> ifFail);

        public static implicit operator ParseResult<TToken, T>(T value) => Okay(value, TokenSequence<TToken>.Empty);

        public static explicit operator T(ParseResult<TToken, T> result) => result switch
        {
            ParseSuccess<TToken, T>(var value, _) => value,
            _ => throw new InvalidCastException($"Cannot faulted result to value of type {typeof(T).Name}"),
        };

        public static implicit operator bool(ParseResult<TToken, T> result) => result.WasSuccessful;

        public static ParseResult<TToken, T> operator &(ParseResult<TToken, T> x, ParseResult<TToken, T> y) => x.And(y);
        public static ParseResult<TToken, T> operator |(ParseResult<TToken, T> x, ParseResult<TToken, T> y) => x.Or(y);
        public static ParseResult<TToken, T> operator ^(ParseResult<TToken, T> x, ParseResult<TToken, T> y) => x.XOr(y);
    }
}
