
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static class IEffectExtensions
    {
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> Select<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, TResult> map)
            => result.Map(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, IEffect<TInput, TResult>> bind)
            => result.AndThen(bind);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TElement, TResult>(this IEffect<TInput, TOutput> result, Func<TOutput, IEffect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> projection)
            => result.AndThen(v => bind(v).Map(x => projection(v, x)));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TOutput> Where<TInput, TOutput>(this IEffect<TInput, TOutput> result, Func<TOutput, bool> predicate) => result.Guard(predicate, _ => Error.Empty);
    }
}