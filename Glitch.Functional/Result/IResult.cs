namespace Glitch.Functional
{
    public interface IResult<T, E>
    {
        bool IsOkay { get; }
        bool IsError { get; }

        IResult<TResult, E> Map<TResult>(Func<T, TResult> map);

        IResult<T, TNewError> MapError<TNewError>(Func<E, TNewError> map);

        IResult<T, E> Guard(Func<T, bool> predicate, Func<T, E> error);

        TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> error);

        // TODO Should these be extension methods?
        virtual TResult Match<TResult>(Func<T, TResult> okay, TResult error) => Match(okay, _ => error);

        virtual IResult<TResult, E> Zip<TOther, TResult>(IResult<TOther, E> other, Func<T, TOther, TResult> zipper)
            => AndThen(x => other.Map(zipper.Curry()(x)));

        virtual T IfFail(T fallback) => Match(Identity, _ => fallback);
        virtual T IfFail(Func<E, T> fallback) => Match(Identity, fallback);

        virtual IResult<TResult, E> Cast<TResult>() => Map(v => (TResult)(dynamic)v!);
        virtual IResult<T, TNewError> CastError<TNewError>() => MapError(v => (TNewError)(dynamic)v!);
        virtual IResult<T, E> Do(Action<T> action)
            => Map(v =>
            {
                action(v);
                return v;
            });

        virtual IResult<TResult, E> And<TResult>(IResult<TResult, E> other)
            => Match(okay: _ => other,
                     error: _ => Cast<TResult>());

        virtual IResult<T, E> Or(IResult<T, E> other)
            => Match(okay: _ => this,
                     error: _ => other);

        virtual IResult<TResult, E> AndThen<TResult>(Func<T, IResult<TResult, E>> bind)
            => Match(okay: bind, error: _ => Cast<TResult>());

        virtual IResult<T, TNewError> OrElse<TNewError>(Func<E, IResult<T, TNewError>> bind)
            => Match(okay: _ => CastError<TNewError>(), error: bind);
    }
}