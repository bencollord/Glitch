namespace Glitch.Functional.Parsing;

internal class LazyParser<TToken, T> : IParser<TToken, T>
{
    private Lazy<IParser<TToken, T>> parser;

    internal LazyParser(Lazy<IParser<TToken, T>> parser)
    {
        this.parser = parser;
    }

    public IParseResult<TToken, T> Execute(ITokenSequence<TToken> input) => parser.Value.Execute(input);
}
