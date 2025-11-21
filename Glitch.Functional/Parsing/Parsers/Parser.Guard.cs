using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, T> Guard(Func<T, bool> predicate)
        => Guard(predicate, Expectation<TToken>.None);

    public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, Expectation<TToken> expectation)
        => Guard(predicate, _ => expectation);

    public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, ParseError<TToken, T> error)
        => Guard(predicate, _ => error);

    public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, Func<T, Expectation<TToken>> expectation)
        => Guard(predicate, t => new ParseError<TToken, T>(expectation(t)));

    public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, Func<T, ParseError<TToken, T>> error)
        => new GuardParser<TToken, T>(this, predicate, error);
}

internal class GuardParser<TToken, T> : Parser<TToken, T>
{
    private readonly Parser<TToken, T> source;
    private readonly Func<T, bool> predicate;
    private readonly Func<T, ParseError<TToken, T>> error;

    internal GuardParser(Parser<TToken, T> source, Func<T, bool> predicate, Func<T, ParseError<TToken, T>> error)
    {
        this.source = source;
        this.predicate = predicate;
        this.error = error;
    }

    public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
    {
        return source.Execute(input)
                     .Match(ok => predicate(ok.Value)
                                ? ok as ParseResult<TToken, T> // The fact that the C# compiler forces this when they're both already derived from this class is ridiculous
                                : error(ok.Value) with { Remaining = input },
                            err => err);
    }
}
