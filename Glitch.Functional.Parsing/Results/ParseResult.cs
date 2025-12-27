using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Results;

public static class ParseResult
{
    public static IParseResult<TToken, T> Empty<TToken, T>() => ParseResult<TToken>.Empty<T>();

    public static IParseResult<TToken, T> Empty<TToken, T>(TokenSequence<TToken> remaining) => ParseResult<TToken>.Empty<T>(remaining);

    public static IParseResult<TToken, T> Okay<TToken, T>(T value) => ParseResult<TToken>.Okay(value);

    public static IParseResult<TToken, T> Okay<TToken, T>(T value, TokenSequence<TToken> remaining) => ParseResult<TToken>.Okay(value, remaining);

    public static IParseResult<TToken, T> Error<TToken, T>(ParseError error) => ParseResult<TToken>.Error<T>(error);

    public static IParseResult<TToken, T> Error<TToken, T>(ParseError error, TokenSequence<TToken> remaining) => ParseResult<TToken>.Error<T>(error, remaining);
}

public static class ParseResult<TToken>
{
    public static IParseResult<TToken, T> Empty<T>() => Error<T>(ParseError.Empty);

    public static IParseResult<TToken, T> Empty<T>(ITokenSequence<TToken> remaining) => Error<T>(ParseError.Empty, remaining);

    public static IParseResult<TToken, T> Okay<T>(T value) => Okay(value, TokenSequence<TToken>.Empty);

    public static IParseResult<TToken, T> Okay<T>(T value, ITokenSequence<TToken> remaining) => new ParseSuccess<TToken, T>(value, remaining);

    public static IParseResult<TToken, T> Error<T>(ParseError error) => Error<T>(error, TokenSequence<TToken>.Empty);

    public static IParseResult<TToken, T> Error<T>(ParseError error, ITokenSequence<TToken> remaining) => new ParseFailure<TToken, T>(error, remaining);
}