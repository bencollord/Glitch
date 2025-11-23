using Glitch.Functional.Collections;
using System.Collections.Immutable;

namespace Glitch.Functional.Effects
{
    public static partial class Effect
    {
        public static Effect<TResult> Apply<T, TResult>(this Effect<Func<T, TResult>> function, Effect<T> value)
            => value.Apply(function);

        public static Effect<T> Flatten<T>(this Effect<Effect<T>> nested)
            => nested.AndThen(n => n);
    }
}
