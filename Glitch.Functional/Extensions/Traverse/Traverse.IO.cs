using Glitch.Functional.Collections;
using Glitch.Functional.Effects;
using System.Collections.Immutable;

namespace Glitch.Functional.Extensions.Traverse;

using IO = Effects.IO;

public static partial class TraverseExtensions
{
    extension<T>(IEnumerable<IO<T>> source)
    {
        // No input
        public IO<Sequence<T>> Traverse()
                => source.Traverse(Identity);

        public IO<Sequence<TResult>> Traverse<TResult>(Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public IO<Sequence<TResult>> Traverse<TResult>(Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.Select(traverse.Apply).Apply(i))
                     .Traverse();
    }

    extension<T>(IEnumerable<T> source)
    {
        public IO<Sequence<TResult>> Traverse<TResult>(Func<T, IO<TResult>> traverse) =>
            source.Aggregate(
                IO.Return(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public IO<Sequence<TResult>> Traverse<TResult>(Func<T, int, IO<TResult>> traverse) =>
            source.Select((s, i) => traverse(s, i)).Traverse();
    }
}
