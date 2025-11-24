namespace Glitch.Functional;

public partial record Result<T, E>
{
    public sealed record Fail(E Error) : Result<T, E>
    {
        public override bool IsOkay => false;

        public override bool IsFail => true;

        /// <inheritdoc />
        public override Result<TResult, E> And<TResult>(Result<TResult, E> other)
            => new Result<TResult, E>.Fail(Error);

        /// <inheritdoc />
        public override Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> mapper)
            => new Result<TResult, E>.Fail(Error);

        /// <inheritdoc />
        public override Result<TResult, E> Select<TResult>(Func<T, TResult> mapper)
            => new Result<TResult, E>.Fail(Error);

        /// <inheritdoc />
        public override Result<T, TNewError> SelectError<TNewError>(Func<E, TNewError> mapper) => mapper(Error);

        /// <inheritdoc />
        public override Result<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> _, Func<E, EResult> fail)
            => new Result<TResult, EResult>.Fail(fail(Error));

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> _, Func<E, TResult> fail)
            => fail(Error);

        /// <inheritdoc />
        public override Result<T, EResult> Or<EResult>(Result<T, EResult> other) => other;

        /// <inheritdoc />
        public override Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> bind) => bind(Error);

        /// <inheritdoc />
        public override Result<T, E> Guard(Func<T, bool> predicate, E _) => this;

        public override Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> _) => this;

        public override string ToString() => $"Error({Error})";
    }
}