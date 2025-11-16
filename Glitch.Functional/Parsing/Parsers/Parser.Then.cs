using Glitch.Functional.Core;
using Glitch.Functional.Parsing.Parsers;

namespace Glitch.Functional.Parsing
{
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
}