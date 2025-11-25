using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    public sealed record Okay(T Value) : Validated<T, E>
    {
        public override bool IsOkay => true;

        public override bool IsFail => false;

        /// <inheritdoc />
        public override Validated<TResult, E> And<TResult>(Validated<TResult, E> other)
            => other;

        /// <inheritdoc />
        public override Validated<TResult, E> AndThen<TResult>(Func<T, Validated<TResult, E>> bind)
            => bind(Value);

        /// <inheritdoc />
        public override Validated<TResult, E> Select<TResult>(Func<T, TResult> map)
            => new Validated<TResult, E>.Okay(map(Value));

        /// <inheritdoc />
        public override Validated<T, EResult> SelectError<EResult>(Func<E, EResult> _) => new Validated<T, EResult>.Okay(Value);

        /// <inheritdoc />
        public override Validated<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> _)
            => new Validated<TResult, EResult>.Okay(okay(Value));

        /// <inheritdoc />
        public override Validated<TResult, E> Apply<TResult>(Validated<Func<T, TResult>, E> function) =>
            function.Select(fn => fn(Value));

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Sequence<E>, TResult> _)
            => ifOkay(Value);

        /// <inheritdoc />
        public override Validated<T, EResult> Or<EResult>(Validated<T, EResult> other) => new Validated<T, EResult>.Okay(Value);

        /// <inheritdoc />
        public override Validated<T, EResult> OrElse<EResult>(Func<Sequence<E>, Validated<T, EResult>> _) => new Validated<T, EResult>.Okay(Value);

        /// <inheritdoc />
        public override Validated<T, E> Coalesce(Validated<T, E> _) => this;

        /// <inheritdoc />
        public override Validated<T, E> Guard(Func<T, bool> predicate, E error)
            => predicate(Value) ? this : error;

        public override Validated<T, E> Guard(Func<T, bool> predicate, Func<T, E> error)
            => predicate(Value) ? this : error(Value);

        public override string ToString() => $"Okay({Value})";

        public override Validated<TResult, E> Zip<TOther, TResult>(Validated<TOther, E> other, Func<T, TOther, TResult> zipper)
            => AndThen(FN<T>.Constant(other), zipper);
    }
}