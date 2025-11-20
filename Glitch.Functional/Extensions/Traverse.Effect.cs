using Glitch.Functional.Collections;
using Glitch.Functional.Effects;
using System.Collections.Immutable;

namespace Glitch.Functional.Extensions;

public static partial class TraverseExtensions
{
    public static Effect<TInput, Sequence<TOutput>> Traverse<TInput, TOutput>(this IEnumerable<Effect<TInput, TOutput>> source)
            => source.Traverse(Identity);

    public static Effect<TInput, Sequence<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<Effect<TInput, TOutput>> source, Func<TOutput, TResult> traverse)
        => source.Traverse(opt => opt.Select(traverse));

    public static Effect<TInput, Sequence<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<TOutput> source, Func<TOutput, Effect<TInput, TResult>> traverse)
        => source.Aggregate(
            Effect<TInput, ImmutableList<TResult>>.Return(ImmutableList<TResult>.Empty),
            (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
            list => list.Select(Sequence.From));

    public static Effect<TInput, Sequence<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<Effect<TInput, TOutput>> source, Func<TOutput, int, TResult> traverse)
        => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                 .Traverse();

    public static Effect<TInput, Sequence<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<TOutput> source, Func<TOutput, int, Effect<TInput, TResult>> traverse)
        => source.Select((s, i) => traverse(s, i))
                 .Traverse();
}
