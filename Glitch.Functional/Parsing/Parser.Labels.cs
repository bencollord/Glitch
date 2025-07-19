namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        private static readonly Prism<ParseError<TToken>, Expectation<TToken>> ExpectationPrism = new(e => e.Expectation, (e, x) => e with { Expectation = x });
        
        private static readonly Prism<Expectation<TToken>, string> LabelPrism = new(e => e.Label, (e, l) => e with { Label = l });

        public Parser<TToken, T> WithLabel(string label)
            => new(input => parser(input).MapError(err => ExpectationPrism.Compose(LabelPrism).Set(err, label)));
    }
}
