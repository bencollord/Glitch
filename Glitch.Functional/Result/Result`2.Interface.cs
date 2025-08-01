namespace Glitch.Functional
{
    public abstract partial record Result<T, E> : IResult<T, E>
    {
        IResult<T, E> IResult<T, E>.Guard(Func<T, bool> predicate, Func<T, E> error) => Guard(predicate, error);

        IResult<TResult, E> IResult<T, E>.Map<TResult>(Func<T, TResult> map) => Map(map);

        IResult<T, TNewError> IResult<T, E>.MapError<TNewError>(Func<E, TNewError> map) => MapError(map);
    }
}