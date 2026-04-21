using Glitch.Functional.Effects;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

/// <summary>
/// Module for text parsing.
/// </summary>
/// <remarks>
/// Contains passthrough methods for <see cref="Parse{TToken}"/> methods,
/// since when I tried to get clever and just inherit from that class,
/// it turns out `using static` only imports declared methods and you'd have
/// to add another explicit statement for the parent methods, which is dumb.
/// </remarks>
public static partial class Parse<TToken>
{
    public static IParser<TToken, T> Error<T>(string message) => Error<T>(ParseError.FromMessage(message));

    public static IParser<TToken, T> Error<T>(ParseError error) => new FailParser<TToken, T>(error);

    public static IParser<TToken, Unit> Error(string message) => Error<Unit>(message);

    public static IParser<TToken, Unit> Error(ParseError error) => Error<Unit>(error);

    public static IParser<TToken, Unit> Guard(bool condition, ParseError error) => Return(Unit.Value).Guard(_ => condition, error);
}