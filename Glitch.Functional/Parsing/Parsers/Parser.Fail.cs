using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    public static Parser<TToken, T> Fail<TToken, T>(Expectation<TToken> expectation)
        => Parser<TToken, T>.Fail(expectation);

    public static Parser<TToken, T> Fail<TToken, T>(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
        => Parser<TToken, T>.Fail(expectation, remaining);

    public static Parser<TToken, T> Fail<TToken, T>(string message)
        => Parser<TToken, T>.Fail(message);

    public static Parser<TToken, T> Fail<TToken, T>(string message, TokenSequence<TToken> remaining)
        => Parser<TToken, T>.Fail(message, remaining);

    public static Parser<TToken, T> Fail<TToken, T>(ParseError<TToken, T> error)
        => Parser<TToken, T>.Fail(error);

    public static Parser<TToken, T> Fail<TToken, T>(ParseError<TToken, T> error, TokenSequence<TToken> remaining)
        => Parser<TToken, T>.Fail(error, remaining);
}

public static partial class Parser<TToken>
{
    public static Parser<TToken, T> Fail<T>(Expectation<TToken> expectation)
        => Parser<TToken, T>.Fail(expectation);

    public static Parser<TToken, T> Fail<T>(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
        => Parser<TToken, T>.Fail(expectation, remaining);

    public static Parser<TToken, T> Fail<T>(string message)
        => Parser<TToken, T>.Fail(message);

    public static Parser<TToken, T> Fail<T>(string message, TokenSequence<TToken> remaining)
        => Parser<TToken, T>.Fail(message, remaining);

    public static Parser<TToken, T> Fail<T>(string message, Expectation<TToken> expectation)
        => Parser<TToken, T>.Fail(message, expectation);

    public static Parser<TToken, T> Fail<T>(string message, Expectation<TToken> expectation, TokenSequence<TToken> remaining)
        => Parser<TToken, T>.Fail(message, expectation, remaining);

    public static Parser<TToken, T> Fail<T>(ParseError<TToken, T> error)
        => Parser<TToken, T>.Fail(error);

    public static Parser<TToken, T> Fail<T>(ParseError<TToken, T> error, TokenSequence<TToken> remaining)
        => Parser<TToken, T>.Fail(error, remaining);
}

public partial class Parser<TToken, T>
{
    public static Parser<TToken, T> Fail(Expectation<TToken> expectation)
        => Fail(new ParseError<TToken, T>(expectation));

    public static Parser<TToken, T> Fail(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
        => Fail(new ParseError<TToken, T>(expectation, remaining));

    public static Parser<TToken, T> Fail(string message)
        => Fail(new ParseError<TToken, T>(message));

    public static Parser<TToken, T> Fail(string message, TokenSequence<TToken> remaining)
        => Fail(new ParseError<TToken, T>(message), remaining);

    public static Parser<TToken, T> Fail(string message, Expectation<TToken> expectation)
        => Fail(new ParseError<TToken, T>(message, expectation));

    public static Parser<TToken, T> Fail(string message, Expectation<TToken> expectation, TokenSequence<TToken> remaining)
        => Fail(new ParseError<TToken, T>(message, expectation), remaining);

    public static Parser<TToken, T> Fail(ParseError<TToken, T> error)
        => new FailParser<TToken, T>(error);

    public static Parser<TToken, T> Fail(ParseError<TToken, T> error, TokenSequence<TToken> remaining)
        => Fail(error).WithRemaining(remaining);
}

/// <summary>
/// A parser that returns an error without consuming input.
/// </summary>
/// <typeparam name="TToken"></typeparam>
/// <typeparam name="T"></typeparam>
internal class FailParser<TToken, T> : Parser<TToken, T>
{
    private ParseError<TToken, T> error;

    internal FailParser(ParseError<TToken, T> error)
    {
        this.error = error;
    }

    public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input) => error with { Remaining = input };
}
