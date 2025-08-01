using Glitch.Functional.Parsing.Input;
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
    }

    public partial class Parser<TToken, T>
    {
        public static Parser<TToken, T> Return(T value)
            => new ReturnParser<TToken, T>(value);

        public static Parser<TToken, T> Return(T value, TokenSequence<TToken> remaining)
            => new ReturnParser<TToken, T>(value, remaining);

        public static Parser<TToken, T> Error(Expectation<TToken> expectation)
            => new ReturnParser<TToken, T>(expectation);

        public static Parser<TToken, T> Error(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
            => new ReturnParser<TToken, T>(expectation, remaining);
        
        public static Parser<TToken, T> Error(string message)
            => Error(Expectation.Labeled<TToken>(message));

        public static Parser<TToken, T> Error(string message, TokenSequence<TToken> remaining)
            => Error(Expectation.Labeled<TToken>(message), remaining);
    }
}
