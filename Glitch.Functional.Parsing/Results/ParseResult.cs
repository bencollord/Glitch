using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Results;

public static class ParseResult
{
    public static IParseResult<TToken, T> Empty<TToken, T>() => ParseResult<TToken>.Empty<T>();

    public static IParseResult<TToken, T> Empty<TToken, T>(ITokenSequence<TToken> remaining) => ParseResult<TToken>.Empty<T>(remaining);

    public static IParseResult<TToken, T> Okay<TToken, T>(T value) => ParseResult<TToken>.Okay(value);

    public static IParseResult<TToken, T> Okay<TToken, T>(T value, ITokenSequence<TToken> remaining) => ParseResult<TToken>.Okay(value, remaining);

    public static IParseResult<TToken, T> Okay<TToken, T>(T value, ParseState<TToken> state) => ParseResult<TToken>.Okay(value, state);

    public static IParseResult<TToken, T> Error<TToken, T>(string message, ITokenSequence<TToken> remaining) => Error<TToken, T>(ParseError.FromMessage(message), remaining);

    public static IParseResult<TToken, T> Error<TToken, T>(ParseError error) => ParseResult<TToken>.Error<T>(error);

    public static IParseResult<TToken, T> Error<TToken, T>(ParseError error, ITokenSequence<TToken> remaining) => ParseResult<TToken>.Error<T>(error, remaining);

    public static IParseResult<TToken, T> Error<TToken, T>(ParseError error, ParseState<TToken> remaining) => ParseResult<TToken>.Error<T>(error, remaining);

    extension<TToken, T>(IParseResult<TToken, T> source)
    {
        public IParseResult<TToken, TResult> Select<TResult>(Func<T, TResult> map)
        {
            return source.Match(okay: v => Okay(map(v), source.Remaining),
                                fail: e => Error<TToken, TResult>(e, source.Remaining));
        }
    }
}

public static class ParseResult<TToken>
{
    public static IParseResult<TToken, T> Empty<T>() => Error<T>(ParseError.Empty);

    public static IParseResult<TToken, T> Empty<T>(ITokenSequence<TToken> remaining) => Error<T>(ParseError.Empty, remaining);

    public static IParseResult<TToken, T> Okay<T>(T value) => Okay(value, TokenSequence<TToken>.Empty);

    public static IParseResult<TToken, T> Okay<T>(T value, ITokenSequence<TToken> remaining) => new ParseSuccess<TToken, T>(value, remaining);

    public static IParseResult<TToken, T> Okay<T>(T value, ParseState<TToken> state) => Okay(value, state.Remaining);

    public static IParseResult<TToken, T> Error<T>(ParseError error) => Error<T>(error, TokenSequence<TToken>.Empty);

    public static IParseResult<TToken, T> Error<T>(ParseError error, ITokenSequence<TToken> remaining) => new ParseFailure<TToken, T>(error, remaining);

    public static IParseResult<TToken, T> Error<T>(ParseError error, ParseState<TToken> state) => Error<T>(error, state.Remaining);
}