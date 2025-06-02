namespace Glitch.Functional
{
    public interface IResult<TSuccess, TError>
    {
        bool IsOkay { get; }
        bool IsFail { get; }

        IResult<TResult, TError> Map<TResult>(Func<TSuccess, TResult> map);
        IResult<TSuccess, TNewError> MapError<TNewError>(Func<TError, TNewError> map);
        IResult<TResult, TError> Cast<TResult>();
        IResult<TSuccess, TError> Do(Action<TSuccess> action);
        IResult<TSuccess, TError> Guard(bool condition, Func<TSuccess, TError> error);
        
        IResult<TResult, TError> And<TResult>(IResult<TResult, TError> other);
        IResult<TResult, TError> AndThen<TResult>(Func<TSuccess, IResult<TResult, TError>> bind);

        IResult<TSuccess, TError> Or(IResult<TSuccess, TError> other);
        IResult<TSuccess, TNewError> OrElse<TNewError>(Func<TError, IResult<TSuccess, TNewError>> other);

        TResult Match<TResult>(Func<TSuccess, TResult> ifOkay, Func<TError, TResult> ifFail);
    }
}