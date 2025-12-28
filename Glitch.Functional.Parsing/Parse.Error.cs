using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse<TToken>
{
    public static IParser<TToken, T> Error<T>(string message) => Error<T>(ParseError.New(message));

    public static IParser<TToken, T> Error<T>(ParseError error) => new FailParser<TToken, T>(error);
}
