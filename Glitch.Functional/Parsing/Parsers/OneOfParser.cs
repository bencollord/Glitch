using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    internal class OneOfParser<TToken, T> : Parser<TToken, T>
    {
        private readonly IEnumerable<Parser<TToken, T>> parsers;

        internal OneOfParser(params IEnumerable<Parser<TToken, T>> parsers)
        {
            this.parsers = parsers;
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
        {
            var expectations = ImmutableList.CreateBuilder<Expectation<TToken>>();

            foreach (var parser in parsers)
            {
                var result = parser.Execute(input);

                if (result.WasSuccessful)
                {
                    return result;
                }

                expectations.Add(result.Expectation);
            }

            // All parsers failed, concatenate the expectations and return
            var labels = string.Join(", ", expectations.Select(e => e.Label));

            return ParseResult<TToken>.Error<T>(
                new Expectation<TToken>
                {
                    Label = $"One of {labels}",
                    Expected = expectations.SelectMany(e => e.Expected)
                },
                input);
        }
    }
}
