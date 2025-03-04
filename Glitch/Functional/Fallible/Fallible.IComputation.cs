namespace Glitch.Functional
{
    public partial class Fallible<T> : IComputation<T>
    {
        object? IComputation<T>.Match() => Run() switch
        {
            Result.Okay<T>(var value) => value,
            Result.Fail<T>(var error) => error,
            _ => throw Result.DiscriminatedUnionViolation()
        };

        IEnumerable<T> IComputation<T>.Iterate()
        {
            // Preserve laziness
            foreach (var item in Run().Iterate())
            {
                yield return item;
            }
        }

        IComputation<TResult> IComputation<T>.AndThen<TResult>(Func<T, IComputation<TResult>> bind)
        {
            return Map(v => bind(v).Iterate())
                .Match<IComputation<TResult>>(
                    Sequence.From,
                    Fallible.Fail<TResult>);
        }

        IComputation<TResult> IComputation<T>.AndThen<TElement, TResult>(Func<T, IComputation<TElement>> bind, Func<T, TElement, TResult> project)
        {
            return ((IComputation<T>)this).AndThen(x => bind(x).Map(y => project(x, y)));
        }

        IComputation<TResult> IComputation<T>.Apply<TResult>(IComputation<Func<T, TResult>> function)
        {
            return ((IComputation<T>)this).AndThen(x => function.Map(fn => fn(x)));
        }

        IComputation<TResult> IComputation<T>.Cast<TResult>()
        {
            return Cast<TResult>();
        }

        IComputation<T> IComputation<T>.Filter(Func<T, bool> predicate)
        {
            return Filter(predicate);
        }

        IComputation<TResult> IComputation<T>.Map<TResult>(Func<T, TResult> map)
        {
            return Map(map);
        }

        IComputation<Func<T2, TResult>> IComputation<T>.PartialMap<T2, TResult>(Func<T, T2, TResult> map)
        {
            return PartialMap(map);
        }
    }
}
