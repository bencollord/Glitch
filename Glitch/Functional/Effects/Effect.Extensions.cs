using System.Collections.Immutable;

namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<Sequence<T>> Traverse<T>(this IEnumerable<Effect<T>> source)
            => source.Traverse(Identity);

        public static Effect<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<Effect<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public static Effect<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Effect<TResult>> traverse)
            => source.Aggregate(
                Return(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public static Effect<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<Effect<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialSelect(traverse).Apply(i))
                     .Traverse();

        public static Effect<Sequence<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Effect<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public static Effect<TResult> Apply<T, TResult>(this Effect<Func<T, TResult>> function, Effect<T> value)
            => value.Apply(function);

        public static Effect<T> Flatten<T>(this Effect<Effect<T>> nested)
            => nested.AndThen(n => n);
    }
}
