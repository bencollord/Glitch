namespace Glitch.Functional.Core;

public sealed record Fail<T, E>(E Error) : Result<T, E>
{
    public override bool IsOkay => false;

    public override bool IsError => true;

    /// <inheritdoc />
    public override Result<TResult, E> And<TResult>(Result<TResult, E> other)
        => new Fail<TResult, E>(Error);

    /// <inheritdoc />
    public override Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> mapper)
        => new Fail<TResult, E>(Error);

    /// <inheritdoc />
    public override Result<TResult, E> Select<TResult>(Func<T, TResult> mapper)
        => new Fail<TResult, E>(Error);

    /// <inheritdoc />
    public override Result<T, TNewError> SelectError<TNewError>(Func<E, TNewError> mapper) => mapper(Error);

    /// <inheritdoc />
    public override TResult Match<TResult>(Func<T, TResult> _, Func<E, TResult> ifFail)
        => ifFail(Error);

    /// <inheritdoc />
    public override Result<T, EResult> Or<EResult>(Result<T, EResult> other) => other;

    /// <inheritdoc />
    public override Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> bindFail) => bindFail(Error);

    /// <inheritdoc />
    public override Result<T, E> Guard(Func<T, bool> predicate, E _) => this;

    public override Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> _) => this;

    public override string ToString() => $"Error({Error})";
}
