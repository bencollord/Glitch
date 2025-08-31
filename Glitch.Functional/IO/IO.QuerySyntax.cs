using Glitch.Functional.Results;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static partial class IOExtensions
    {
        public static IO<TResult> SelectMany<T, TResult>(this IO<T> source, Func<T, Task> action, Func<T, Unit, TResult> project)
            => source.SelectAsync(async x =>
            {
                await action(x).ConfigureAwait(false);

                return project(x, Nothing);
            });

        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Task<TElement>> bind, Func<T, TElement, TResult> project)
            => source.SelectAsync(async x =>
            {
                var v = await bind(x).ConfigureAwait(false);

                return project(x, v);
            });

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, IO<TElement>> bind, Func<T, TElement, TResult> project) 
            => source.AndThen(bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Task<IO<TElement>>> bind, Func<T, TElement, TResult> project)
            => source.AndThenAsync(bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IO<TResult> SelectMany<T, E, TElement, TResult>(this IO<T> source, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Effect<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this IO<T> source, Func<T, Effect<TEnv, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);
    }
}