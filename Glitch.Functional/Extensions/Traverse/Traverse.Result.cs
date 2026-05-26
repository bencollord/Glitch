using Glitch.Functional;
using Glitch.Functional.Collections;
using Glitch.Functional.Errors;
using System.Collections.Immutable;

namespace Glitch.Functional.Extensions.Traverse;

public static partial class TraverseExtensions
{
    extension<T, E>(IEnumerable<Result<T, E>> source)
    {
        public Result<Sequence<T>, E> Traverse()
        => source.Traverse(Identity);

        public Result<Sequence<TResult>, E> Traverse<TResult>(Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public Result<Sequence<TResult>, E> Traverse<TResult>(Func<T, int, TResult> traverse)
            => source.Select((s, i) => s * traverse * Result.Okay<int, E>(i))
                     .Traverse();
    }

    extension<T>(IEnumerable<T> source)
    {
        public Result<Sequence<TResult>, E> Traverse<E, TResult>(Func<T, int, Result<TResult, E>> traverse)
        => source.Select((s, i) => traverse(s, i))
                 .Traverse();

        public Result<Sequence<TResult>, E> Traverse<E, TResult>(Func<T, Result<TResult, E>> traverse)
            => source.Aggregate(
                Result.Okay<ImmutableList<TResult>, E>(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Sequence.From));

        public Result<Unit, E> Traverse<E>(Func<T, Result<Unit, E>> traverse)
            => source.Traverse<T, E, Unit>(s => traverse(s)).Select(Unit.Ignore);
    }
}
