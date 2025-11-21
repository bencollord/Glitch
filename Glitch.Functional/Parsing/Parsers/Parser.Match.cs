using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> okay, Func<ParseError<TToken, T>, TResult> error)
        => Match(ok => ParseResult.Okay(okay(ok), ok.Remaining),
                 err => ParseResult.Okay(error(err), err.Remaining));

    public virtual Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> okay, Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> error)
        => new MatchParser<TToken, T, TResult>(this, okay, error);
}

internal class MatchParser<TToken, T, TResult> : Parser<TToken, TResult>
{
    private readonly Parser<TToken, T> source;
    private readonly Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> okay;
    private readonly Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> error;

    public MatchParser(Parser<TToken, T> source, Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> okay, Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> error)
    {
        this.source = source;
        this.okay = okay;
        this.error = error;
    }

    public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input) => source.Execute(input).Match(okay, error);
}