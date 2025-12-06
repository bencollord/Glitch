
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Effects;

public static class IEffectExtensions
{
    extension<TInput, TOutput>(IEffect<TInput, TOutput> source)
    {
        public IEffect<TInput, TResult> SelectMany<TElement, TResult>(Func<TOutput, IEffect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> projection) => 
            source.AndThen(v => bind(v).Select(x => projection(v, x)));

        public IEffect<TInput, TOutput> Do(Action<TOutput> action) => source.Select(x => action.Return(x)(x));
    }
}