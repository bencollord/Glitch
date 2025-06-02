using System.Collections.Immutable;

namespace Glitch.Functional
{
    public abstract partial record Result<T> : IResult<T, Error>
    {
        IResult<TResult, Error> IResult<T, Error>.And<TResult>(IResult<TResult, Error> other)
            => IsOkay ? other : Cast<TResult>();

        IResult<TResult, Error> IResult<T, Error>.AndThen<TResult>(Func<T, IResult<TResult, Error>> bind) 
            => Match(bind, _ => Cast<TResult>());

        IResult<TResult, Error> IResult<T, Error>.Cast<TResult>() => Cast<TResult>();

        IResult<T, Error> IResult<T, Error>.Do(Action<T> action) => Do(action);

        IResult<T, Error> IResult<T, Error>.Guard(bool condition, Func<T, Error> error) => Guard(condition, error);

        IResult<TResult, Error> IResult<T, Error>.Map<TResult>(Func<T, TResult> map) => Map(map);

        IResult<T, TNewError> IResult<T, Error>.MapError<TNewError>(Func<Error, TNewError> map)
        {
            // TODO
            throw new NotImplementedException();
        }

        IResult<T, Error> IResult<T, Error>.Or(IResult<T, Error> other)
        {
            // TODO
            throw new NotImplementedException();
        }

        IResult<T, TNewError> IResult<T, Error>.OrElse<TNewError>(Func<Error, IResult<T, TNewError>> other)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}