using Glitch.Functional;
using Glitch.Functional.Errors;
using System.Collections.Immutable;

namespace Glitch.Functional.Extensions.Traverse;

public static partial class TraverseExtensions
{
    extension<T>(IEnumerable<Expected<T>> source)
    {
        public Expected<IEnumerable<T>> Traverse()
            => source.Traverse(Identity);

        public Expected<IEnumerable<TResult>> Traverse<TResult>(Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public Expected<IEnumerable<TResult>> Traverse<TResult>(Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.Select(traverse.Curry()).Select(fn => fn(i))) // TODO Make this work with operators!!
                     .Traverse();
    }

    extension<T>(IEnumerable<T> source)
    {
        public Expected<IEnumerable<TResult>> Traverse<TResult>(Func<T, int, Expected<TResult>> traverse)
        => source.Select((s, i) => traverse(s, i))
                 .Traverse();

        public Expected<IEnumerable<TResult>> Traverse<TResult>(Func<T, Expected<TResult>> traverse)
            => source.Aggregate(
                Expected.Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Enumerable.AsEnumerable));

        public Expected<Unit> Traverse(Func<T, Expected<Unit>> traverse)
            => source.Traverse<T, Unit>(traverse).Select(Unit.Ignore);
    }
}
