using Glitch.Functional.Collections;
using Glitch.Functional.Effects;
using System.Collections.Immutable;

namespace Glitch.Functional.Extensions.Traverse;

public static partial class TraverseExtensions
{
    extension<TState, T>(IEnumerable<State<TState, T>> source)
    {
        public State<TState, Sequence<T>> Traverse()
            // HACK The fact that the explict Func conversion here is needed to avoid
            // ambiguity errors is probably a design smell.
            => source.Traverse(Func<State<TState, T>, State<TState, T>>(m => m));

        public State<TState, Sequence<TResult>> Traverse<TResult>(Func<T, TResult> traverse)
            => source.Traverse(opt => opt * traverse);

        public State<TState, Sequence<TResult>> Traverse<TResult>(Func<T, int, TResult> traverse)
            => source.Select((s, i) => s * traverse.Apply * State<TState>.Return(i))
                     .Traverse();
    }

    extension<TState, T, TResult>(IEnumerable<T> source)
    {
        public State<TState, Sequence<TResult>> Traverse(Func<T, State<TState, TResult>> traverse)
            => source.Aggregate(
                State<TState>.Return(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list * Sequence.From);

        public State<TState, Sequence<TResult>> Traverse(Func<T, int, State<TState, TResult>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public State<TState, Unit> Traverse(Func<T, State<TState, Unit>> traverse)
            => source.Traverse(s => traverse(s)) * Unit.Ignore;
    }
}
