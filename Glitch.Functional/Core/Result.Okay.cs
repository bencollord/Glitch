namespace Glitch.Functional.Core
{
    public sealed record Okay<T, E>(T Value) : Result<T, E>
    {
        public override bool IsOkay => true;

        public override bool IsError => false;

        /// <inheritdoc />
        public override Result<TResult, E> And<TResult>(Result<TResult, E> other)
            => other;

        /// <inheritdoc />
        public override Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> mapper)
            => mapper(Value);

        /// <inheritdoc />
        public override Result<TResult, E> Select<TResult>(Func<T, TResult> mapper)
            => new Okay<TResult, E>(mapper(Value));

        /// <inheritdoc />
        public override Result<T, TNewError> SelectError<TNewError>(Func<E, TNewError> _) => Result<T, TNewError>.Okay(Value);

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> ifOkay, Func<E, TResult> _)
            => ifOkay(Value);

        /// <inheritdoc />
        public override Result<T, EResult> Or<EResult>(Result<T, EResult> other) => new Okay<T, EResult>(Value);

        /// <inheritdoc />
        public override Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> _) => new Okay<T, EResult>(Value);

        /// <inheritdoc />
        public override Result<T, E> Guard(Func<T, bool> predicate, E error)
            => predicate(Value) ? this : error;

        public override Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> error)
            => predicate(Value) ? this : error(Value);

        public override string ToString() => $"Okay({Value})";
    }
}