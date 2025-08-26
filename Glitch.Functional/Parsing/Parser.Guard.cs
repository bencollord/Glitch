using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, T> Guard(Func<T, bool> predicate)
            => Guard(predicate, Expectation<TToken>.None);

        public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, Expectation<TToken> expectation)
            => Guard(predicate, _ => expectation);

        public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, ParseError<TToken, T> error)
            => Guard(predicate, _ => error);

        public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, Func<T, Expectation<TToken>> expectation)
            => Guard(predicate, t => new ParseError<TToken, T>(expectation(t)));

        public virtual Parser<TToken, T> Guard(Func<T, bool> predicate, Func<T, ParseError<TToken, T>> error)
            => new GuardParser<TToken, T>(this, predicate, error);
    }
}