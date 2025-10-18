using Glitch.Functional.Results;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<IEnumerable<T>> Traverse<T>(this IEnumerable<Expected<T>> source)
            => source.Traverse(Identity);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Expected<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Expected<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Expected<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Expected<TResult>> traverse)
            => source.Aggregate(
                Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(l => l.AsEnumerable()));

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
