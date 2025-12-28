using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

internal class MatchParser<TToken, T, TResult> : IParser<TToken, TResult>
{
    private readonly IParser<TToken, T> source;
    private readonly Func<ParseSuccess<TToken, T>, IParseResult<TToken, TResult>> okay;
    private readonly Func<ParseFailure<TToken, T>, IParseResult<TToken, TResult>> error;

    public MatchParser(IParser<TToken, T> source, Func<ParseSuccess<TToken, T>, IParseResult<TToken, TResult>> okay, Func<ParseFailure<TToken, T>, IParseResult<TToken, TResult>> error)
    {
        this.source = source;
        this.okay = okay;
        this.error = error;
    }

    public IParseResult<TToken, TResult> Execute(ITokenSequence<TToken> input)
    {
        var result = source.Execute(input);

        // HACK IParseResult is fundamentally a discriminated union. It's only an interface because I want
        // the return type T to be covariant. Because of that though, we can't match on the concrete types
        // of ParseSuccess and ParseFailure in case the interface gets implemented somewhere else. 
        // Still deciding on some of the design here, so for now, we'll just safely fall back to matching on the result
        // and propagating the remaining input into new instances if the type isn't a match.
        return result switch
        {
            ParseSuccess<TToken, T> succ => okay(succ),
            ParseFailure<TToken, T> fail => error(fail),
            _ => result.Match(okay: v => okay(new ParseSuccess<TToken, T>(v, result.Remaining)),
                              fail: e => error(new ParseFailure<TToken, T>(e, result.Remaining)))
        };
    }
}