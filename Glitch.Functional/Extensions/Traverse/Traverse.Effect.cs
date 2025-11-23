using Glitch.Functional.Collections;
using Glitch.Functional.Effects;
using Glitch.Functional.Errors;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Glitch.Functional.Extensions.Traverse;

[DebuggerStepThrough]
public static partial class TraverseExtensions
{
    extension<T>(IEnumerable<Effect<T>> source)
    {
        // No input
        public Effect<Sequence<T>> Traverse()
                => source.Traverse(Identity);

        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();
    }

    extension<T>(IEnumerable<T> source)
    {
        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, Effect<TResult>> traverse) =>
            source.Aggregate(
                Effect.Return(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public Effect<Sequence<TResult>> Traverse<TResult>(Func<T, int, Effect<TResult>> traverse) =>
            source.Select((s, i) => traverse(s, i)).Traverse();
    }

    extension<TInput, TOutput>(IEnumerable<Effect<TInput, TOutput>> source)
    {
        // With input
        public Effect<TInput, Sequence<TOutput>> Traverse()
                => source.Traverse(Identity);

        public Effect<TInput, Sequence<TResult>> Traverse<TResult>(Func<TOutput, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public Effect<TInput, Sequence<TResult>> Traverse<TResult>(Func<TOutput, int, TResult> traverse)
            => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();
    }

    extension<TOutput>(IEnumerable<TOutput> source)
    {
        public Effect<TInput, Sequence<TResult>> Traverse<TInput, TResult>(Func<TOutput, Effect<TInput, TResult>> traverse)
            => source.Aggregate(
                Effect<TInput, ImmutableList<TResult>>.Return(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public Effect<TInput, Sequence<TResult>> Traverse<TInput, TResult>(Func<TOutput, int, Effect<TInput, TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public Effect<TInput, Unit> Traverse<TInput>(Func<TOutput, Effect<TInput, Unit>> traverse)
            => source.Traverse<TOutput, TInput, Unit>(s => traverse(s)).Select(Unit.Ignore);
    }
}
