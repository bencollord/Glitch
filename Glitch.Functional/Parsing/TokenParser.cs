using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    using static Parse;

    public class TokenParser<TToken> : Parser<TToken, TToken>
    {
        private Func<TToken, bool> predicate;
        private Expectation<TToken> expectation;

        internal TokenParser(TToken token)
            : this(t => t!.Equals(token), Expectation.Expected(token)) { }

        internal TokenParser(Func<TToken, bool> predicate, Expectation<TToken> expectation)
        {
            this.predicate = predicate;
            this.expectation = expectation;
        }

        public override SeparatedByContext<TToken, TToken, TToken> SeparatedBy(TToken token)
        {
            return new SeparatedByContext<TToken, TToken, TToken>(
                parser: Except(token),
                separator: Token(token));
        }

        public override SeparatedByContext<TToken, TToken, TSeparator> SeparatedBy<TSeparator>(Parser<TToken, TSeparator> separator)
        {
            return new SeparatedByContext<TToken, TToken, TSeparator>(
                parser: from _ in separator.Not()
                        from tkn in this
                        select tkn,
                separator: separator);
        }

        public TokenParser<TToken> Except(TToken token) => WithPredicate(x => predicate(x) && !x.Equals(token));

        public TokenParser<TToken> Except(Func<TToken, bool> predicate) => WithPredicate(x => this.predicate(x) && !predicate(x));

        public override TokenParser<TToken> WithExpectation(Expectation<TToken> expectation)
            => new(predicate, expectation);

        public override TokenParser<TToken> WithLabel(string label)
            => WithExpectation(e => e with { Label = label });

        public override TokenParser<TToken> WithExpected(TToken expected)
            => WithExpected([expected]);

        public override TokenParser<TToken> WithExpected(params IEnumerable<TToken> expected)
            => WithExpectation(e => e with { Expected = expected });

        private TokenParser<TToken> WithExpectation(Func<Expectation<TToken>, Expectation<TToken>> updater)
            => WithExpectation(updater(expectation));

        private TokenParser<TToken> WithPredicate(Func<TToken, bool> predicate) => new(predicate, expectation);
        public override ParseResult<TToken, TToken> Execute(TokenSequence<TToken> input)
        {
            return predicate(input.Current)
                 ? ParseResult.Okay(input.Current, input.Advance())
                 : ParseResult.Error<TToken, TToken>(expectation with { Unexpected = input.Current }, input);
        }
    }
}
