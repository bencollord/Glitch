using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public static partial class Parser
    {
        public static Parser<TToken, T> Return<TToken, T>(T value)
            => Parser<TToken, T>.Return(value);

        public static Parser<TToken, T> Return<TToken, T>(T value, TokenSequence<TToken> remaining)
           => Parser<TToken, T>.Return(value, remaining);

        public static Parser<TToken, T> Error<TToken, T>(Expectation<TToken> expectation)
            => Parser<TToken, T>.Error(expectation);

        public static Parser<TToken, T> Error<TToken, T>(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
            => Parser<TToken, T>.Error(expectation, remaining);

        public static Parser<TToken, T> Error<TToken, T>(string message)
            => Parser<TToken, T>.Error(message);

        public static Parser<TToken, T> Error<TToken, T>(string message, TokenSequence<TToken> remaining)
            => Parser<TToken, T>.Error(message, remaining);

        public static Parser<TToken, T> Error<TToken, T>(ParseError<TToken, T> error)
            => Parser<TToken, T>.Error(error);

        public static Parser<TToken, T> Error<TToken, T>(ParseError<TToken, T> error, TokenSequence<TToken> remaining)
            => Parser<TToken, T>.Error(error, remaining);
    }

    public static partial class Parser<TToken>
    {
        public static Parser<TToken, T> Return<T>(T value)
           => Parser<TToken, T>.Return(value);

        public static Parser<TToken, T> Return<T>(T value, TokenSequence<TToken> remaining)
           => Parser<TToken, T>.Return(value, remaining);

        public static Parser<TToken, T> Error<T>(Expectation<TToken> expectation)
            => Parser<TToken, T>.Error(expectation);

        public static Parser<TToken, T> Error<T>(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
            => Parser<TToken, T>.Error(expectation, remaining);

        public static Parser<TToken, T> Error<T>(string message)
            => Parser<TToken, T>.Error(message);

        public static Parser<TToken, T> Error<T>(string message, TokenSequence<TToken> remaining)
            => Parser<TToken, T>.Error(message, remaining);

        public static Parser<TToken, T> Error<T>(ParseError<TToken, T> error)
            => Parser<TToken, T>.Error(error);

        public static Parser<TToken, T> Error<T>(ParseError<TToken, T> error, TokenSequence<TToken> remaining)
            => Parser<TToken, T>.Error(error, remaining);
    }

    public partial class Parser<TToken, T>
    {
        public static Parser<TToken, T> Return(T value)
            => Return(ParseResult<TToken>.Okay(value));

        public static Parser<TToken, T> Return(T value, TokenSequence<TToken> remaining)
            => Return(ParseResult.Okay(value, remaining));

        public static Parser<TToken, T> Return(ParseResult<TToken, T> result)
            => new ReturnParser<TToken, T>(result);

        public static Parser<TToken, T> Error(Expectation<TToken> expectation)
            => Error(new ParseError<TToken, T>(expectation));

        public static Parser<TToken, T> Error(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
            => Error(new ParseError<TToken, T>(expectation, remaining));

        public static Parser<TToken, T> Error(string message)
            => Error(new ParseError<TToken, T>(message));

        public static Parser<TToken, T> Error(string message, TokenSequence<TToken> remaining)
            => Error(new ParseError<TToken, T>(message), remaining);

        public static Parser<TToken, T> Error(ParseError<TToken, T> error)
            => Return(error);

        public static Parser<TToken, T> Error(ParseError<TToken, T> error, TokenSequence<TToken> remaining)
            => Return(error with { Remaining = remaining });
    }
}
