namespace Glitch.Functional;

public interface IResult<T, E>
{
    bool IsOkay { get; }
    bool IsFail { get; }

    TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> fail);
}
