using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, T> WithRemaining(TokenSequence<TToken> remaining)
        => Match(ok => ok with { Remaining = remaining },
                 err => err with { Remaining = remaining });

    public virtual Parser<TToken, T> WithExpectation(Expectation<TToken> expectation)
        => WithExpectation(_ => expectation);

    public virtual Parser<TToken, T> WithLabel(string label)
        => WithExpectation(e => e with { Label = label });

    public virtual Parser<TToken, T> WithExpected(TToken expected)
        => WithExpected([expected]);

    public virtual Parser<TToken, T> WithExpected(params IEnumerable<TToken> expected)
        => WithExpectation(e => e with { Expected = expected });

    private Parser<TToken, T> WithExpectation(Func<Expectation<TToken>, Expectation<TToken>> update)
        => Match(ok => ok with { Expectation = update(ok.Expectation) },
                 err => err with { Expectation = update(err.Expectation) });
}