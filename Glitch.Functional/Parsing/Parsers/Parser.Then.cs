using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, TOther> Then<TOther>(Parser<TToken, TOther> other)
        => Then(_ => other);

    public Parser<TToken, T> Then(Parser<TToken, Unit> other)
        => Then(_ => other, (x, _) => x);

    public virtual Parser<TToken, TResult> Then<TElement, TResult>(Parser<TToken, TElement> next, Func<T, TElement, TResult> projection)
        => Then(x => next.Select(y => projection(x, y)));

    public virtual Parser<TToken, TResult> Then<TResult>(Func<T, Parser<TToken, TResult>> next)
        => Then(next, (_, nxt) => nxt);

    public virtual Parser<TToken, T> Then(Func<T, Parser<TToken, Unit>> next)
        => Then(next, (x, _) => x);

    public virtual Parser<TToken, TResult> Then<TElement, TResult>(Func<T, Parser<TToken, TElement>> selector, Func<T, TElement, TResult> projection)
        => new BindParser<TToken, T, TElement, TResult>(this, selector, projection);

}

internal class BindParser<TToken, TSource, TNext, TResult> : Parser<TToken, TResult>
{
    private readonly Parser<TToken, TSource> source;
    private readonly Func<TSource, Parser<TToken, TNext>> next;
    private readonly Func<TSource, TNext, TResult> projection;

    internal BindParser(Parser<TToken, TSource> source, Func<TSource, Parser<TToken, TNext>> next, Func<TSource, TNext, TResult> projection)
    {
        this.source = source;
        this.next = next;
        this.projection = projection;
    }

    public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
    {
        var result = source.Execute(input);

        return result.Match(
            okay => next(okay.Value)
                .Execute(okay.Remaining)
                .Select(nxt => projection(okay.Value, nxt)),
            err => err.Cast<TResult>());
    }
}