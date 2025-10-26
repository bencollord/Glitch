using Glitch.Functional.Results;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<TResult> Apply<T, TResult>(this Expected<Func<T, TResult>> function, Expected<T> value)
            => value.Apply(function);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> Flatten<T>(this Expected<Expected<T>> nested)
            => nested.AndThen(n => n);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> Select<T>(this Expected<bool> result, Func<Unit, T> @true, Func<Unit, T> @false)
            => result.Select(flag => flag ? @true(default) : @false(default));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> Select<T>(this Expected<bool> result, Func<T> @true, Func<T> @false)
            => result.Select(flag => flag ? @true() : @false());
    }
}
