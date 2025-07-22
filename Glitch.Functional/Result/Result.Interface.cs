namespace Glitch.Functional
{
    public abstract partial record Result<T> : IResult<T, Error>
    {
        IResult<T, Error> IResult<T, Error>.Guard(Func<T, bool> predicate, Func<T, Error> error) => Guard(predicate, error);

        IResult<TResult, Error> IResult<T, Error>.Map<TResult>(Func<T, TResult> map) => Map(map);

        IResult<T, TNewError> IResult<T, Error>.MapError<TNewError>(Func<Error, TNewError> map)
        {
            return Match(Result<T, TNewError>.Okay, e => Result<T, TNewError>.Fail(map(e)));
        }
    }
}