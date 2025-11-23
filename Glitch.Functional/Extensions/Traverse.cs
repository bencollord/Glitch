using Glitch.Functional.Collections;
using Glitch.Functional.Core;
using Glitch.Functional.Errors;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Extensions
{
    public static partial class TraverseExtensions
    {
        // Result
        // ========================================================================================
        public static Result<Sequence<T>, E> Traverse<T, E>(this IEnumerable<Result<T, E>> source)
            => source.Traverse(Identity);

        public static Result<Sequence<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<Result<T, E>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public static Result<Sequence<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<Result<T, E>> source, Func<T, int, TResult> traverse)
            => source//.Select((s, i) => s * traverse * Result.Okay(i)) // TODO See if we can make this work!!
                     .Select((s, i) => s.Select(traverse.Curry()).Select(fn => fn(i)))
                     .Traverse();

        public static Result<Sequence<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<T> source, Func<T, int, Result<TResult, E>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public static Result<Sequence<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<T> source, Func<T, Result<TResult, E>> traverse)
            => source.Aggregate(
                Result<ImmutableList<TResult>, E>.Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        // Option
        // ========================================================================================
        public static Option<Sequence<T>> Traverse<T>(this IEnumerable<Option<T>> source)
            => source.Traverse(Identity);

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<Option<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Option<TResult>> traverse)
            => source.Aggregate(
                Option.Some(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<Option<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.Select(traverse.Curry()).Select(fn => fn(i))) // TODO Make this work with operators!!
                     .Traverse();

        public static Option<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Option<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();


        // Expected
        // ========================================================================================
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Sequence<T>> Traverse<T>(this IEnumerable<Expected<T>> source)
            => source.Traverse(Identity);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<Expected<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<Expected<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.Select(traverse.Curry()).Select(fn => fn(i))) // TODO Make this work with operators!!
                     .Traverse();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Expected<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Expected<TResult>> traverse)
            => source.Aggregate(
                Expected.Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));
    }
}
