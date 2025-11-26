using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static TokenParser<TToken> OneOf<TToken>(params IEnumerable<TToken> tokens)
        => Satisfy<TToken>(t => tokens.Contains(t));

    public static Parser<TToken, T> OneOf<TToken, T>(params IEnumerable<Parser<TToken, T>> parsers)
        => new OneOfParser<TToken, T>(parsers);

    private class OneOfParser<TToken, T> : Parser<TToken, T>
    {
        private readonly IEnumerable<Parser<TToken, T>> parsers;

        internal OneOfParser(params IEnumerable<Parser<TToken, T>> parsers)
        {
            this.parsers = parsers;
        }

        public override Parser<TToken, T> Or(Parser<TToken, T> other)
            => new OneOfParser<TToken, T>([.. parsers, other]);

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
        {
            var expectations = new List<Expectation<TToken>>();

            foreach (var parser in parsers)
            {
                var result = parser.Execute(input);

                if (result.IsOkay)
                {
                    return result;
                }

                expectations.Add(result.Expectation);
            }

            var expectation = new Expectation<TToken>
            {
                Label = "One of " + expectations.Select(x => x.Label).Somes().PipeInto(e => string.Join(", ", e)),
                Expected = expectations.SelectMany(e => e.Expected)
            };

            return ParseResult.Error<TToken, T>(expectation, input);
        }
    }

}
