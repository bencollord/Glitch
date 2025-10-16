
namespace Glitch.Functional.Results
{
    public interface IResult
    {
        bool IsError { get; }
        bool IsOkay { get; }
    }

    public interface IResult<out T, out E> : IResult
    {
        IResult<TResult, E> Select<TResult>(Func<T, TResult> map);
        IResult<T, EResult> SelectError<EResult>(Func<E, EResult> map);

        virtual IResult<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);
        virtual IResult<T, EResult> CastError<EResult>() => SelectError(DynamicCast<EResult>.From);

        T Unwrap();
        E UnwrapError();
    }
}