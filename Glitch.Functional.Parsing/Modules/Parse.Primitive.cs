using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public partial class Parse<TToken>
{
    public static IParser<TToken, Unit> EndOfInput =>
        from state in State
        from check in state.IsEnd
                    ? Return(Unit.Value)
                    : Error<Unit>(ParseError.New("Expected EOF"))
        select Unit.Value;

    public static IParser<TToken, T> Return<T>(T value) => State.Then(s => Return(ParseResult.Okay(value, s)));

    public static IParser<TToken, T> Return<T>(IParseResult<TToken, T> result) => new ReturnParser<TToken, T>(result);

    public static IParser<TToken, T> Error<T>(string message) => Error<T>(ParseError.New(message));

    public static IParser<TToken, T> Error<T>(ParseError error) => new FailParser<TToken, T>(error);

    public static IParser<TToken, Unit> Not<T>(IParser<TToken, T> parser) => parser.Not();

    public static IParser<TToken, T> OneOf<T>(params IEnumerable<IParser<TToken, T>> parsers) => new OneOfParser<TToken, T>(parsers);

    public static IParser<TToken, IEnumerable<T>> Sequence<T>(IEnumerable<IParser<TToken, T>> parsers) => new SequenceParser<TToken, T, IEnumerable<T>>(parsers, FN.Identity);
}
