namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, T> Or(IParser<TToken, T> other) => Parse<TToken>.OneOf(source, other);

        public IParser<TToken, T> Except<TOther>(IParser<TToken, TOther> other) => other.Not().Then(source);

        public IParser<TToken, Unit> Not() => new NegatedParser<TToken, T>(source);

        public IParser<TToken, TOther> Then<TOther>(IParser<TToken, TOther> other) => source.Then(_ => other);

        public IParser<TToken, T> Then(IParser<TToken, Unit> other) => source.Then(_ => other, (x, _) => x);

        public IParser<TToken, TResult> Then<TElement, TResult>(IParser<TToken, TElement> next, Func<T, TElement, TResult> projection) =>
            source.Then(x => next.Select(y => projection(x, y)));

        public IParser<TToken, T> Then(Func<T, IParser<TToken, Unit>> next) =>
            source.Then(next, (x, _) => x);

        public IParser<TToken, TResult> Then<TResult>(Func<T, IParser<TToken, TResult>> next) => 
            new ThenParser<TToken, T, TResult>(source, next);

        public IParser<TToken, TResult> Then<TElement, TResult>(Func<T, IParser<TToken, TElement>> next, Func<T, TElement, TResult> projection) =>
            source.Then(x => next(x).Select(projection.Apply(x)));
    }
}