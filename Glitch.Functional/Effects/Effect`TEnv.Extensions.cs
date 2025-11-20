using Glitch.Functional.Collections;
using System.Collections.Immutable;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static Effect<TInput, TResult> Apply<TInput, TOutput, TResult>(this Effect<TInput, Func<TOutput, TResult>> function, Effect<TInput, TOutput> value)
            => value.Apply(function);

        public static Effect<TInput, TOutput> Flatten<TInput, TOutput>(this Effect<TInput, Effect<TInput, TOutput>> nested)
            => nested.AndThen(n => n);
    }
}
