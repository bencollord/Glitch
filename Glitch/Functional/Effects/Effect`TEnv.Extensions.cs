using System.Collections.Immutable;

namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<TInput, IEnumerable<TOutput>> Traverse<TInput, TOutput>(this IEnumerable<Effect<TInput, TOutput>> source)
            => source.Traverse(Identity);

        public static Effect<TInput, IEnumerable<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<Effect<TInput, TOutput>> source, Func<TOutput, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Effect<TInput, IEnumerable<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<TOutput> source, Func<TOutput, Effect<TInput, TResult>> traverse)
            => source.Aggregate(
                Effect<TInput, ImmutableList<TResult>>.Return(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));

        public static Effect<TInput, IEnumerable<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<Effect<TInput, TOutput>> source, Func<TOutput, int, TResult> traverse)
            => source.Select((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Effect<TInput, IEnumerable<TResult>> Traverse<TInput, TOutput, TResult>(this IEnumerable<TOutput> source, Func<TOutput, int, Effect<TInput, TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public static Effect<TInput, TResult> Apply<TInput, TOutput, TResult>(this Effect<TInput, Func<TOutput, TResult>> function, Effect<TInput, TOutput> value)
            => value.Apply(function);

        public static Effect<TInput, TOutput> Flatten<TInput, TOutput>(this Effect<TInput, Effect<TInput, TOutput>> nested)
            => nested.AndThen(n => n);
    }
}
