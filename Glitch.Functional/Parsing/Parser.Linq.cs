using Glitch.Functional.Parsing.Results;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Parsing
{
    public static partial class Parser
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Parser<TToken, TResult> Select<TToken, T, TResult>(this Parser<TToken, T> parser, Func<T, TResult> selector)
            => parser.Map(selector);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Parser<TToken, TResult> SelectMany<TToken, T, TResult>(this Parser<TToken, T> parser, Func<T, Parser<TToken, TResult>> selector)
            => parser.Then(selector);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Parser<TToken, TResult> SelectMany<TToken, T, TElement, TResult>(this Parser<TToken, T> parser, Func<T, Parser<TToken, TElement>> selector, Func<T, TElement, TResult> projection)
            => parser.Then(selector, projection);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Parser<TToken, T> Where<TToken, T>(this Parser<TToken, T> parser, Func<T, bool> predicate)
            => parser.Guard(predicate);
    }
}
