namespace Glitch.Functional;

public partial record Result<T, E>
{
    public sealed record Okay(T Value) : Result<T, E>
    {
        public override bool IsOkay => true;

        public override bool IsFail => false;

        /// <inheritdoc />
        public override Result<TResult, E> And<TResult>(Result<TResult, E> other)
            => other;

        /// <inheritdoc />
        public override Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> mapper)
            => mapper(Value);

        /// <inheritdoc />
        public override Result<TResult, E> Select<TResult>(Func<T, TResult> mapper)
            => new Result<TResult, E>.Okay(mapper(Value));

        /// <inheritdoc />
        public override Result<T, TNewError> SelectError<TNewError>(Func<E, TNewError> _) => new Result<T, TNewError>.Okay(Value);

        /// <inheritdoc />
        public override Result<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> fail)
            => new Result<TResult, EResult>.Okay(okay(Value));

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> ifOkay, Func<E, TResult> _)
            => ifOkay(Value);

        /// <inheritdoc />
        public override Result<T, EResult> Or<EResult>(Result<T, EResult> other) => new Result<T, EResult>.Okay(Value);

        /// <inheritdoc />
        public override Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> _) => new Result<T, EResult>.Okay(Value);

        /// <inheritdoc />
        public override Result<T, E> Guard(Func<T, bool> predicate, E error)
            => predicate(Value) ? this : error;

        public override Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> error)
            => predicate(Value) ? this : error(Value);

        public override string ToString() => $"Okay({Value})";
    }
}