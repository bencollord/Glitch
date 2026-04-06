namespace Glitch.Functional;

public interface IResult<out T, out E> : IMaybe<T>
{
    bool IsOkay { get; }
    bool IsFail { get; }

    bool IMaybe.HasValue => IsOkay;

    TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> fail);

    TResult IMaybe<T>.Match<TResult>(Func<T, TResult> some, Func<TResult> none) => Match(okay: some, fail: (E _) => none());
}
