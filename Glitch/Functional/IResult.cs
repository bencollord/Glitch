using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public interface IResult
    {
        bool IsError { get; }
        bool IsOkay { get; }
    }

    public interface IResultFactory<T, E>
    {
        static abstract IResult<T, E> Okay(T value);
        static abstract IResult<T, E> Fail(E error);
    }

    public interface IResult<out T, out E> : IResult
    {
        TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> error);

        virtual IResult<TResult, E> Select<TResult>(Func<T, TResult> map) 
            => Match(v => Result.Okay(map(v)).ToResult<E>(), e => Result.Fail(e).ToResult<TResult>());
        virtual IResult<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => Match(v => Result.Okay(v).ToResult<EResult>(), e => Result.Fail(map(e)).ToResult<T>());

        virtual IResult<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);
        virtual IResult<T, EResult> CastError<EResult>() => SelectError(DynamicCast<EResult>.From);
    }
}