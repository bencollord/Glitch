using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class MaybeParser<TToken, T> : IParser<TToken, Option<T>>
{
    private readonly IParser<TToken, T> source;

    internal MaybeParser(IParser<TToken, T> source)
    {
        this.source = source;
    }

    public IParseResult<TToken, Option<T>> Execute(ITokenSequence<TToken> input)
    {
        var result = source.Execute(input);

        return result.Match(
            v => ParseResult.Okay(Option<T>.Some(v), result.Remaining),
            _ => ParseResult.Okay(Option<T>.None, input));
    }
}
