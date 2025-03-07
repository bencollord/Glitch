namespace Glitch.Functional
{
    public abstract partial record Validation<T> : IComputation<T>
    {
        object? IComputation<T>.Match() => Match<object?>(val => val, err => err);

        IComputation<TResult> IComputation<T>.AndThen<TResult>(Func<T, IComputation<TResult>> bind)
        {
            return this switch
            {
                Validation.Success<T>(var value) => bind(value),
                Validation.Failure<T>(var error) => Fail<TResult>(error),
                _ => throw new NotSupportedException("You're not supposed to be extending the Validation<T> class")
            };
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
    };
}
