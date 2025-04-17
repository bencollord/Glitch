using System.Collections.Immutable;

namespace Glitch.Functional
{
    public static partial class Fallible
    {
        public static Fallible<IEnumerable<T>> Traverse<T>(this IEnumerable<Fallible<T>> source)
            => source.Traverse(Identity);

        public static Fallible<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Fallible<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Fallible<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Fallible<TResult>> traverse)
            => source.Aggregate(
                Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));

        public static Fallible<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Fallible<T>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Fallible<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, int, Fallible<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public static Fallible<TResult> Apply<T, TResult>(this Fallible<Func<T, TResult>> function, Fallible<T> value)
            => value.Apply(function);

        public static Fallible<T> Flatten<T>(this Fallible<Fallible<T>> nested)
            => nested.AndThen(n => n);
    }
}
