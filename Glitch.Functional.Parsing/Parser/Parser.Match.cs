using Glitch.Functional.Parsing.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Parse;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> okay, Func<ParseFailure<TToken, T>, TResult> fail)
            => source.Match(ok => ParseResult.Okay(okay(ok), ok.Remaining),
                            err => ParseResult.Okay(fail(err), err.Remaining));

        public IParser<TToken, TResult> Match<TResult>(Func<ParseSuccess<TToken, T>, IParseResult<TToken, TResult>> okay, Func<ParseFailure<TToken, T>, IParseResult<TToken, TResult>> fail)
            => new MatchParser<TToken, T, TResult>(source, okay, fail);
    }
}