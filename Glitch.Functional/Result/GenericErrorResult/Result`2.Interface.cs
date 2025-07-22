namespace Glitch.Functional
{
    public abstract partial record Result<TOkay, TError> : IResult<TOkay, TError>
    {
        IResult<TOkay, TError> IResult<TOkay, TError>.Guard(Func<TOkay, bool> predicate, Func<TOkay, TError> error) => Guard(predicate, error);

        IResult<TResult, TError> IResult<TOkay, TError>.Map<TResult>(Func<TOkay, TResult> map) => Map(map);

        IResult<TOkay, TNewError> IResult<TOkay, TError>.MapError<TNewError>(Func<TError, TNewError> map) => MapError(map);
    }
}