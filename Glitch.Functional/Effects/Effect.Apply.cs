using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Effects
{
    [DebuggerStepThrough]
    public static partial class Effect
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> Apply<TInput, T, TResult>(this IEffect<TInput, T> source, IEffect<TInput, Func<T, TResult>> function)
            => function.Apply(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEffect<TInput, TResult> Apply<TInput, T, TResult>(this IEffect<TInput, Func<T, TResult>> source, IEffect<TInput, T> value)
            => source.AndThen(func => value.Select(v => func(v)));
    }
}
