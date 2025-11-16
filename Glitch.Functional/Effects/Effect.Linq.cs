
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static IEffect<TInput, TResult> Select<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> source, Func<TOutput, TResult> map)
            => source.Select(map);

        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TResult>(this IEffect<TInput, TOutput> source, Func<TOutput, IEffect<TInput, TResult>> bind)
            => source.AndThen(bind);

        public static IEffect<TInput, TResult> SelectMany<TInput, TOutput, TElement, TResult>(this IEffect<TInput, TOutput> source, Func<TOutput, IEffect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> projection)
            => source.AndThen(v => bind(v).Select(x => projection(v, x)));
    }
}