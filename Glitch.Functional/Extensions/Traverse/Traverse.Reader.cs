using Glitch.Functional.Effects;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Glitch.Functional.Extensions.Traverse;

public static partial class TraverseExtensions
{
    extension<TInput, TOutput>(IEnumerable<Reader<TInput, TOutput>> source)
    {
        // With input
        public Reader<TInput, IEnumerable<TOutput>> Traverse()
                => source.Traverse((Reader<TInput, TOutput> s) => s);

        public Reader<TInput, IEnumerable<TResult>> Traverse<TResult>(Func<TOutput, TResult> traverse)
            => source.Traverse(opt => opt.Select(traverse));

        public Reader<TInput, IEnumerable<TResult>> Traverse<TResult>(Func<TOutput, int, TResult> traverse)
            => source.Select((s, i) => s.Select(x => traverse(x, i)))
                     .Traverse();
    }

    extension<TOutput>(IEnumerable<TOutput> source)
    {
        public Reader<TInput, IEnumerable<TResult>> Traverse<TInput, TResult>(Func<TOutput, Reader<TInput, TResult>> traverse)
            => source.Aggregate(
                Reader<TInput, ImmutableList<TResult>>.Return(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Select(Enumerable.AsEnumerable));

        public Reader<TInput, IEnumerable<TResult>> Traverse<TInput, TResult>(Func<TOutput, int, Reader<TInput, TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public Reader<TInput, Unit> Traverse<TInput>(Func<TOutput, Reader<TInput, Unit>> traverse)
            => source.Traverse<TOutput, TInput, Unit>(s => traverse(s)).Select(Unit.Ignore);
    }
}
