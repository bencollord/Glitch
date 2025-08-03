using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> okay, Func<ParseError<TToken, T>, TResult> error)
            => Match(ok => ParseResult.Okay(okay(ok), ok.Remaining),
                     err => ParseResult.Okay(error(err), err.Remaining));

        public virtual Parser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, ParseResult<TToken, TResult>> okay, Func<ParseError<TToken, T>, ParseResult<TToken, TResult>> error)
            => new MatchParser<TToken, T, TResult>(this, okay, error);
    }
}