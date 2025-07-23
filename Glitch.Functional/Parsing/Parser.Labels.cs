using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        private static readonly Prism<ParseResult<TToken, T>, Expectation<TToken>> ExpectationPrism = new(e => e.Expectation, (e, x) => e with { Expectation = x });

        private static readonly Prism<ParseResult<TToken, T>, string> LabelPrism = ExpectationPrism.Compose<string>(new(e => e.Label, (e, l) => e with { Label = l }));

        public Parser<TToken, T> WithLabel(string label)
            => WithExpectation(e => e with { Label = label });

        public Parser<TToken, T> WithExpected(TToken expected)
            => WithExpected([expected]);

        public Parser<TToken, T> WithExpected(params IEnumerable<TToken> expected)
            => WithExpectation(e => e with { Expected = expected });

        private Parser<TToken, T> WithExpectation(Func<Expectation<TToken>, Expectation<TToken>> update)
            => new(input => parser(input).Match(
                                ExpectationPrism.Update(update),
                                ExpectationPrism.Update(update)));
    }
}
