using Glitch.Functional;
using Glitch.Functional.Collections;
using Glitch.Functional.Errors;
using System.Collections.Immutable;

namespace Glitch.Functional.Extensions.Traverse;

public static partial class TraverseExtensions
{
    extension<T>(IEnumerable<Option<T>> source)
    {
        public Option<Sequence<T>> Traverse()
        => source.Traverse(Identity);

        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.Select(traverse.Curry()).Select(fn => fn(i))) // TODO Make this work with operators!!
                     .Traverse();
    }

    extension<T>(IEnumerable<T> source)
    {
        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, Option<TResult>> traverse)
            => source.Aggregate(
                Option.Some(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public Option<Sequence<TResult>> Traverse<TResult>(Func<T, int, Option<TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public Option<Unit> Traverse(Func<T, Option<Unit>> traverse)
            => source.Traverse<T, Unit>(traverse).Select(Unit.Ignore);
    }
}
