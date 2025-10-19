namespace Glitch.Functional.Results
{
    /**
     * Since <see cref="IEither{T, E}"/> is experimental and involves
     * some messiness with explicit interface implementations, I'm isolating
     * all of the implementation stuff that's in flux here so it's easily accessible.
     */
    public readonly partial struct Option<T> : IEither<T, Unit>
    {
        bool IEither.IsError => IsNone;
        bool IEither.IsOkay => IsSome;

        IEither<TResult, Unit> IEither<T, Unit>.Select<TResult>(Func<T, TResult> map) => Select(map);
        IEither<T, EResult> IEither<T, Unit>.SelectError<EResult>(Func<Unit, EResult> map) => Match(Result.Okay<T, EResult>, unit => Result.Fail<T, EResult>(map(unit)));
    }

    public partial record Result<T, E> : IEither<T, E>
    {
        IEither<TResult, E> IEither<T, E>.Select<TResult>(Func<T, TResult> map) => Select(map);
        IEither<T, EResult> IEither<T, E>.SelectError<EResult>(Func<E, EResult> map) => SelectError(map);
    }

    public partial record Expected<T> : IEither<T, Error>
    {
        IEither<TResult, Error> IEither<T, Error>.Select<TResult>(Func<T, TResult> map) => Select(map);
        IEither<T, EResult> IEither<T, Error>.SelectError<EResult>(Func<Error, EResult> map) => Match(Result.Okay<T, EResult>, unit => Result.Fail<T, EResult>(map(unit)));
    }
}