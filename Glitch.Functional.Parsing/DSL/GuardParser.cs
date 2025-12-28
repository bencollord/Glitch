using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class GuardParser<TToken, T> : IParser<TToken, T>
{
    private readonly IParser<TToken, T> source;
    private readonly Func<T, bool> predicate;
    private readonly Func<T, ParseError> error;

    internal GuardParser(IParser<TToken, T> source, Func<T, bool> predicate, Func<T, ParseError> error)
    {
        this.source = source;
        this.predicate = predicate;
        this.error = error;
    }

    public IParseResult<TToken, T> Execute(ITokenSequence<TToken> input)
    {
        var result = source.Execute(input);

        return result.Match(ok => predicate(ok)
                                ? ParseResult.Okay(ok, result.Remaining)
                                : ParseResult.Error<TToken, T>(error(ok), input),
                            err => ParseResult.Error<TToken, T>(err, input));
    }
}
