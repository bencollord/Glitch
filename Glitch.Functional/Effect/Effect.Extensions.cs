using System.Collections.Immutable;

namespace Glitch.Functional
{
    public static partial class Effect
    {
        public static Effect<IEnumerable<T>> Traverse<T>(this IEnumerable<Effect<T>> source)
            => source.Traverse(Identity);

        public static Effect<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Effect<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Effect<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Effect<TResult>> traverse)
            => source.Aggregate(
                Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));

        public static Effect<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Effect<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Effect<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Effect<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public static Effect<TResult> Apply<T, TResult>(this Effect<Func<T, TResult>> function, Effect<T> value)
            => value.Apply(function);

        public static Effect<T> Flatten<T>(this Effect<Effect<T>> nested)
            => nested.AndThen(n => n);
    }
}
