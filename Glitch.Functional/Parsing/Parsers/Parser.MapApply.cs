using Glitch.Functional.Parsing.Parsers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Parser<TToken, TResult> Select<TResult>(Func<T, TResult> selector)
            => new MapParser<TToken, T, TResult>(this, selector);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Parser<TToken, TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        public virtual Parser<TToken, TResult> Return<TResult>(TResult value) => Then(Parser<TToken, TResult>.Return(value));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Parser<TToken, Unit> IgnoreResult()
            => Select(_ => Nothing);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Parser<TToken, TResult> Apply<TResult>(Parser<TToken, Func<T, TResult>> apply)
            => Then(x => apply.Select(y => y(x)));
    }
}