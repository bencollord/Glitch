using Glitch.Functional.Attributes;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public static class ResultQuerySyntax
    {
        // Result
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(s => bind(s).Select(e => project(s, e)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project)
            => source.Select(s => project(s, bind(s).Value));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Fail<E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(s => Result.Fail<TResult, E>(bind(s).Error));

        // Expected
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<TResult> SelectMany<T, TElement, TResult>(this Expected<T> source, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<TResult> SelectMany<T, TElement, TResult>(this Expected<T> source, Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project)
            => source.Select(s => project(s, bind(s).Value));

        // Option
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<TResult> SelectMany<T, TElement, TResult>(this Option<T> source, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<TResult> Join<T, TOther, TKey, TResult>(this Option<T> left, Option<TOther> right, Func<T, TKey> leftKeySelector, Func<TOther, TKey> rightKeySelector, Func<T, TOther, TResult> resultSelector)
            => left.Zip(right,
                    (x, y) => leftKeySelector(x)!.Equals(rightKeySelector(y))
                            ? Option.Some(resultSelector(x, y))
                            : Option<TResult>.None)
                   .Flatten();
    }
}