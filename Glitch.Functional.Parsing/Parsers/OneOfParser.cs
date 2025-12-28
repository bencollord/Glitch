using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class OneOfParser<TToken, T> : IParser<TToken, T>
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
