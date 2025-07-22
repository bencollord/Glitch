namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        private static readonly Prism<ParseResult<TToken, T>, Expectation<TToken>> ExpectationPrism = new(e => e.Expectation, (e, x) => e with { Expectation = x });

        private static readonly Prism<ParseResult<TToken, T>, string> LabelPrism = ExpectationPrism.Compose<string>(new(e => e.Label, (e, l) => e with { Label = l }));

        public Parser<TToken, T> WithLabel(string label)
            => new(input => parser(input).Match(
                                ok => LabelPrism.Set(ok, label),
                                err => LabelPrism.Set(err, label)));

        public Parser<TToken, T> WithExpectation(Expectation<TToken> expectation)
            => new(input => parser(input).Match(
                                ok => ExpectationPrism.Set(ok, expectation), 
                                err => ExpectationPrism.Set(err, expectation)));
    }
}
