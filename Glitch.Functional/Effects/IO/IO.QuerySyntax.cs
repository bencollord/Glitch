using Glitch.Functional;
using Glitch.Functional.Errors;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Effects
{
    public static partial class IOExtensions
    {
        public static IO<TResult> SelectMany<T, TResult>(this IO<T> source, Func<T, Task> action, Func<T, Unit, TResult> project)
            => source.SelectAsync(async x =>
            {
                await action(x).ConfigureAwait(false);

                return project(x, Unit.Value);
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
        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IO<TResult> SelectMany<T, E, TElement, TResult>(this IO<T> source, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);
    }
}