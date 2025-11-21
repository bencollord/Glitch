using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    /// <summary>
    /// Runs the current parser with a <paramref name="slice">function</paramref>
    /// that receives a <see cref="ReadOnlySpan{T}"/> of all the matched tokens, 
    /// along with the current <typeparamref name="T">parsed result</typeparamref>.
    /// </summary>
    /// <remarks>
    /// This is straight up lifted from Pidgin's method of the same name here:
    /// https://github.com/benjamin-hodgson/Pidgin/blob/main/Pidgin/Parser.Slice.cs
    /// 
    /// According to the documentation, this allows you to write "pattern-style" parsers,
    /// which I personally have never heard of and the docs don't go into much detail about them.
    /// Adding it to my own library to experiment with and see if I can figure it out.
    /// </remarks>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="slice"></param>
    /// <returns></returns>
    public virtual Parser<TToken, TResult> Slice<TResult>(Func<ReadOnlySpan<TToken>, T, TResult> slice)
    {
        return new SliceParser<TToken, T, TResult>(this, slice);
    }
}

internal class SliceParser<TToken, T, TResult> : Parser<TToken, TResult>
{
    private readonly Parser<TToken, T> parser;
    private readonly Func<ReadOnlySpan<TToken>, T, TResult> slice;

    internal SliceParser(Parser<TToken, T> parser, Func<ReadOnlySpan<TToken>, T, TResult> slice)
    {
        this.parser = parser;
        this.slice = slice;
    }

    public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
    {
        var parseResult = parser.Execute(input);

        if (parseResult is ParseSuccess<TToken, T> ok)
        {
            var delta = parseResult.Remaining.Position - input.Position;
            var consumed = parseResult.Remaining.Lookback(delta);
            var result = slice(consumed, ok.Value);

            return ParseResult<TToken, TResult>.Okay(result, parseResult.Remaining);
        }

        return parseResult.Cast<TResult>();
    }
}