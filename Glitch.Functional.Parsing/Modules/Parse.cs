using Glitch.Functional.Effects;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

/// <summary>
/// Module for text parsing.
/// </summary>
/// <remarks>
/// Contains passthrough methods for <see cref="Parse{char}"/> methods,
/// since when I tried to get clever and just inherit from that class,
/// it turns out `using static` only imports declared methods and you'd have
/// to add another explicit statement for the parent methods, which is dumb.
/// </remarks>
public partial class Parse
{
    public static IParser<char, ParseState<char>> State => Parse<char>.State;

    public static IParser<char, Unit> EndOfInput => Parse<char>.EndOfInput;

    public static IParser<char, T> Return<T>(T value) => Parse<char>.Return(value);

    public static IParser<char, T> Return<T>(IParseResult<char, T> result) => Parse<char>.Return(result);

    public static IParser<char, T> Error<T>(string message) => Parse<char>.Error<T>(message);

    public static IParser<char, T> Error<T>(ParseError error) => Parse<char>.Error<T>(error);

    public static IParser<char, Unit> Not<T>(IParser<char, T> parser) => Parse<char>.Not(parser);

    public static IParser<char, T> OneOf<T>(params IEnumerable<IParser<char, T>> parsers) => Parse<char>.OneOf(parsers);

    public static IParser<char, IEnumerable<T>> Sequence<T>(IEnumerable<IParser<char, T>> parsers) => Parse<char>.Sequence(parsers);

    public static IParser<char, T> Ref<T>(Func<IParser<char, T>> function) => Parse<char>.Ref(function);

    public static IParser<char, T> Ref<T>(Lazy<IParser<char, T>> lazy) => Parse<char>.Ref(lazy);

    /// <inheritdoc cref="Parse{TToken}.Rec{T}(Func{IParser{TToken, T}, IParser{TToken, T}})"/>
    public static IParser<char, T> Rec<T>(Func<IParser<char, T>, IParser<char, T>> function) => Parse<char>.Rec(function);

    public static IParser<char, T> Trace<T>(IParser<char, T> parser) => parser.Trace();
}