using Glitch.Functional;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class MapParser<TToken, T, TResult> : IParser<TToken, TResult>
{
    private readonly IParser<TToken, T> source;
    private readonly Func<T, TResult> projection;

    internal MapParser(IParser<TToken, T> source, Func<T, TResult> projection)
    {
        this.source = source;
        this.projection = projection;
    }

    public IParseResult<TToken, TResult> Execute(ITokenSequence<TToken> input)
    {
        return source.Execute(input).Select(projection);
    }
}
