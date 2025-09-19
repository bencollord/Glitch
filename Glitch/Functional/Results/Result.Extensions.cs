using Glitch.Functional.Results;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public static partial class Result
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<IEnumerable<T>> Traverse<T>(this IEnumerable<Result<T>> source)
            => source.Traverse(Identity);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Result<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Result<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Result<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Result<TResult>> traverse)
            => source.Aggregate(
                Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(l => l.AsEnumerable()));

        /// <summary>
        /// Returns a the unwrapped values of all the successful results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Successes<T>(this IEnumerable<Result<T>> results)
            => results.Where(IsOkay).Select(r => (T)r);

        /// <summary>
        /// Returns a the unwrapped errors of all the faulted results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Error> Errors<T>(this IEnumerable<Result<T>> results)
            => results.Where(IsFail).Select(r => (Error)r);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (IEnumerable<T> Successes, IEnumerable<Error> Errors) Partition<T>(this IEnumerable<Result<T>> results)
            => (results.Successes(), results.Errors());

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TResult> Apply<T, TResult>(this Result<Func<T, TResult>> function, Result<T> value)
            => value.Apply(function);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Flatten<T>(this Result<Result<T>> nested)
            => nested.AndThen(n => n);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T> Select<T>(this Result<bool> result, Func<Unit, T> @true, Func<Unit, T> @false)
            => result.Select(flag => flag ? @true(default) : @false(default));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Match<T>(this Result<bool> result, Func<Unit, T> @true, Func<Unit, T> @false, Func<Error, T> error)
            => result.Match(flag => flag ? @true(default) : @false(default), error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Unit Match(this Result<bool> result, Action @true, Action @false, Action<Error> error)
            => result.Match(flag => flag ? @true.Return()() : @false.Return()(), error.Return());
    }
}
