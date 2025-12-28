using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static IParser<TToken, T> Return<TToken, T>(T value) => new ReturnParser<TToken, T>(value);

    /// <summary>
    /// A parser that returns a result without consuming input.
    /// </summary>
    /// <typeparam name="TToken"></typeparam>
    /// <typeparam name="T"></typeparam>
    private class ReturnParser<TToken, T> : IParser<TToken, T>
    {
        private T result;

        internal ReturnParser(T result)
        {
            this.result = result;
        }

        public IParseResult<TToken, T> Execute(ITokenSequence<TToken> input) => ParseResult.Okay(result, input);
    }

}

public static partial class Parse<TToken>
{
    public static IParser<TToken, T> Return<T>(T value) => Parse.Return<TToken, T>(value);
}