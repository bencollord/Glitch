namespace Glitch.Functional;

public interface IResult<T, E> : IMaybe<T>
{
    bool IsOkay { get; }
    bool IsFail { get; }

    bool IMaybe<T>.HasValue => IsOkay;

    TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> fail);

    T IMaybe<T>.UnwrapOrElse(Func<T> fallback) => Match(Identity, _ => fallback());
}
