using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Parse;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, T> Or(IParser<TToken, T> other) => OneOf(source, other);

        public IParser<TToken, TResult> Apply<TResult>(IParser<TToken, Func<T, TResult>> apply) => source.Then(x => apply.Select(y => y(x)));

        public IParser<TToken, T> Except<TOther>(IParser<TToken, TOther> other) => other.Not().Then(source);

        public IParser<TToken, Unit> Not() => new NegatedParser<TToken, T>(source);

        public IParser<TToken, T> Before([DisallowNull] TToken token) => source.Before(Token(token));

        public IParser<TToken, T> Before<TOther>(IParser<TToken, TOther> parser) => source.Then(parser, (me, _) => me);

        public IParser<TToken, T> After([DisallowNull] TToken token) => source.After(Token(token));

        public IParser<TToken, T> After<TOther>(IParser<TToken, TOther> parser) => parser.Then(source, (_, me) => me);

        public IParser<TToken, T> Between<TSeparator>(TToken separator) => source.Between(separator, separator);

        public IParser<TToken, T> Between<TStart, TStop>(TStart start, TStop stop) => source.Between(Token(start), Token(stop));

        public IParser<TToken, T> Between<TSeparator>(IParser<TToken, TSeparator> separator) => source.Between(separator, separator);

        public IParser<TToken, T> Between<TStart, TStop>(IParser<TToken, TStart> start, IParser<TToken, TStop> stop) =>
            from s in start
            from x in source
            from e in stop
            select x;

        public IParser<TToken, TOther> Then<TOther>(IParser<TToken, TOther> other) => source.Then(_ => other);

        public IParser<TToken, T> Then(IParser<TToken, Unit> other) => source.Then(_ => other, (x, _) => x);

        public IParser<TToken, TResult> Then<TElement, TResult>(IParser<TToken, TElement> next, Func<T, TElement, TResult> projection) =>
            source.Then(x => next.Select(y => projection(x, y)));

        public IParser<TToken, T> Then(Func<T, IParser<TToken, Unit>> next) =>
            source.Then(next, (x, _) => x);

        public IParser<TToken, TResult> Then<TResult>(Func<T, IParser<TToken, TResult>> next) => 
            new BindParser<TToken, T, TResult>(source, next);

        public IParser<TToken, TResult> Then<TElement, TResult>(Func<T, IParser<TToken, TElement>> next, Func<T, TElement, TResult> projection) =>
            source.Then(x => next(x).Select(projection.Apply(x)));
    }
}