
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static class IEffectExtensions
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> Select<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, TResult> map)
            => result.Select(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, IEffect<TInput, TResult>> bind)
            => result.AndThen(bind);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TElement, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, IEffect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> projection)
            => result.AndThen(v => bind(v).Select(x => projection(v, x)));
    }
}