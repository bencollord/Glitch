using Glitch.Functional.Parsing.Extensions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class OneOfParser<TToken, T> : Parser<TToken, T>
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
                Label = "One of " + expectations.Select(x => x.Label).Somes().Join(", "),
                Expected = expectations.SelectMany(e => e.Expected)
            };

            return ParseResult.Error<TToken, T>(expectation, input);
        }
    }
}
