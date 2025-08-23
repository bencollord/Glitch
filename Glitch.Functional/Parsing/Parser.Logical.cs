using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, T> Or(Parser<TToken, T> other)
            => new OneOfParser<TToken, T>(this, other);

        public virtual Parser<TToken, Unit> Not()
            => new NegatedParser<TToken, T>(this);

        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success and a successful result containing
        /// <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <returns></returns>
        public virtual Parser<TToken, Option<T>> Maybe()
            => Match(ok => ParseResult.Okay(Some(ok.Value), ok.Remaining),
                     err => ParseResult.Okay(Option<T>.None, err.Remaining));
    }
}