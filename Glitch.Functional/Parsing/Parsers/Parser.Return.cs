using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    public static Parser<TToken, T> Return<TToken, T>(T value)
        => Parser<TToken, T>.Return(value);

    public static Parser<TToken, T> Return<TToken, T>(T value, TokenSequence<TToken> remaining)
       => Parser<TToken, T>.Return(value, remaining);
}

public static partial class Parser<TToken>
{
    public static Parser<TToken, T> Return<T>(T value)
       => Parser<TToken, T>.Return(value);

    public static Parser<TToken, T> Return<T>(T value, TokenSequence<TToken> remaining)
       => Parser<TToken, T>.Return(value, remaining);
}

public partial class Parser<TToken, T>
{
    public static Parser<TToken, T> Return(T value) => new ReturnParser<TToken, T>(value);

    public static Parser<TToken, T> Return(T value, TokenSequence<TToken> remaining)
        => Return(value).WithRemaining(remaining);
}

/// <summary>
/// A parser that returns a result without consuming input.
/// </summary>
/// <typeparam name="TToken"></typeparam>
/// <typeparam name="T"></typeparam>
internal class ReturnParser<TToken, T> : Parser<TToken, T>
{
    private T result;

    internal ReturnParser(T result)
    {
        this.result = result;
    }

    public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input) => ParseResult.Okay(result, input);
}
