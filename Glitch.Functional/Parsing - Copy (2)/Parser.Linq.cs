using System.Diagnostics;

namespace Glitch.Functional.Parsing
{
    public static partial class Parser
    {
        [DebuggerStepThrough]
        public static Parser<TResult> Select<T, TResult>(this Parser<T> parser, Func<T, TResult> selector)
            => parser.Map(selector);

        [DebuggerStepThrough]
        public static Parser<TResult> SelectMany<T, TResult>(this Parser<T> parser, Func<T, Parser<TResult>> selector)
            => parser.AndThen(selector);

        [DebuggerStepThrough]
        public static Parser<TResult> SelectMany<T, TElement, TResult>(this Parser<T> parser, Func<T, Parser<TElement>> selector, Func<T, TElement, TResult> projection)
            => parser.AndThen(selector, projection);

        [DebuggerStepThrough]
        public static ParseResult<TResult> Select<T, TResult>(this ParseResult<T> result, Func<T, TResult> selector)
            => result.Map(selector);

        [DebuggerStepThrough]
        public static ParseResult<TResult> SelectMany<T, TResult>(this ParseResult<T> result, Func<T, ParseResult<TResult>> selector)
            => result.AndThen(selector);

        [DebuggerStepThrough]
        public static ParseResult<TResult> SelectMany<T, TElement, TResult>(this ParseResult<T> result, Func<T, ParseResult<TElement>> selector, Func<T, TElement, TResult> projection)
            => result.AndThen(selector, projection);
    }
}
