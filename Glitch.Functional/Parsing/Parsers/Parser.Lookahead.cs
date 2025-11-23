using Glitch.Functional;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    /// <summary>
    /// Returns a new parser that returns an <see cref="Option{T}"/>
    /// on success without consuming any input and a successful result
    /// containing <see cref="Option{T}.None"/> on failure.
    /// </summary>
    /// <remarks>
    /// Like <see cref="Maybe()"/>, but does not advance the input further
    /// </remarks>
    /// <returns></returns>
    public virtual Parser<TToken, Option<T>> Lookahead()
        => new LookaheadParser<TToken, T>(this);
}

internal class LookaheadParser<TToken, T> : Parser<TToken, Option<T>>
{
    private readonly Parser<TToken, T> parser;

    public LookaheadParser(Parser<TToken, T> parser)
    {
        this.parser = parser;
    }

    public override ParseResult<TToken, Option<T>> Execute(TokenSequence<TToken> input)
    {
        return parser.Execute(input)
                     .Match(ok => ParseResult.Okay(Option.Some(ok.Value), input), // Backtrack
                            err => ParseResult.Okay(Option<T>.None, input));
    }
}