using Glitch.Functional.Parsing.Results;
using Glitch.Functional.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success and a successful result containing
        /// <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <returns></returns>
        public virtual Parser<TToken, Option<T>> Maybe()
            => Match(ok => ParseResult.Okay(Option.Some(ok.Value), ok.Remaining),
                     err => ParseResult.Okay(Option<T>.None, err.Remaining));
    }

    public static partial class ParserExtensions
    {
        public static Parser<TToken, T> IfNone<TToken, T>(this Parser<TToken, Option<T>> parser, T fallback) => parser.Select(p => p.IfNone(fallback));
        public static Parser<TToken, T> IfNone<TToken, T>(this Parser<TToken, Option<T>> parser, Func<T> fallback) => parser.Select(p => p.IfNone(fallback));
        public static Parser<TToken, T> IfNone<TToken, T>(this Parser<TToken, Option<T>> parser, Func<Unit, T> fallback) => parser.Select(p => p.IfNone(fallback));
    }
}