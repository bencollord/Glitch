using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Parser<TToken, TResult> SelectMany<TElement, TResult>(Func<T, Parser<TToken, TElement>> selector, Func<T, TElement, TResult> projection)
            => Then(selector, projection);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Parser<TToken, T> Where(Func<T, bool> predicate)
            => Guard(predicate);
    }
}
