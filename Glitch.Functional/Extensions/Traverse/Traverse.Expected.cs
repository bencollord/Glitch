using Glitch.Functional;
using Glitch.Functional.Collections;
using Glitch.Functional.Errors;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Glitch.Functional.Extensions.Traverse;

public static partial class TraverseExtensions
{
    extension<T>(IEnumerable<Result<T>> source)
    {
        public Result<Sequence<T>> Traverse()
            => source.Traverse(Identity);

        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.Select(traverse.Curry()).Select(fn => fn(i))) // TODO Make this work with operators!!
                     .Traverse();
    }

    extension<T>(IEnumerable<T> source)
    {
        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, int, Result<TResult>> traverse)
        => source.Select((s, i) => traverse(s, i))
                 .Traverse();

        public Result<Sequence<TResult>> Traverse<TResult>(Func<T, Result<TResult>> traverse)
            => source.Aggregate(
                Result.Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public Result<Unit> Traverse(Func<T, Result<Unit>> traverse)
            => source.Traverse<T, Unit>(traverse).Select(Unit.Ignore);
    }
}
