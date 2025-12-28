using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static ITokenParser<TToken> OneOf<TToken>(params IEnumerable<TToken> tokens) =>
        Satisfy<TToken>(t => tokens.Contains(t));

    public static IParser<TToken, TToken> OneOf<TToken>(params IEnumerable<ITokenParser<TToken>> parsers) =>
        new OneOfParser<TToken, TToken>(parsers);

    public static IParser<TToken, T> OneOf<TToken, T>(params IEnumerable<IParser<TToken, T>> parsers) =>
        new OneOfParser<TToken, T>(parsers);

    private class OneOfParser<TToken, T> : IParser<TToken, T>
    {
        private readonly IEnumerable<IParser<TToken, T>> parsers;

        internal OneOfParser(params IEnumerable<IParser<TToken, T>> parsers)
        {
            this.parsers = parsers;
        }

        public IParseResult<TToken, T> Execute(ITokenSequence<TToken> input)
        {
            var expectations = new List<string>();

            foreach (var parser in parsers)
            {
                var result = parser.Execute(input);

                if (result.IsFail(out var error))
                {
                    expectations.AddRange(error.Expectations);
                }
                else
                {
                    return result;
                }
            }

            return ParseResult.Error<TToken, T>(ParseError.Expected(expectations), input);
        }
    }
}
