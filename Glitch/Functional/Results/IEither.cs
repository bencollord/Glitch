namespace Glitch.Functional.Results
{
    public interface IEither
    {
        bool IsError { get; }
        bool IsOkay { get; }
    }

    public interface IEither<out T, out E> : IEither
    {
        TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> error);

        virtual IEither<TResult, E> Select<TResult>(Func<T, TResult> map) 
            => Match(v => Result.Okay(map(v)).ToResult<E>(), e => Result.Fail(e).ToResult<TResult>());
        virtual IEither<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => Match(v => Result.Okay(v).ToResult<EResult>(), e => Result.Fail(map(e)).ToResult<T>());

        virtual IEither<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);
        virtual IEither<T, EResult> CastError<EResult>() => SelectError(DynamicCast<EResult>.From);
    }
}