namespace Glitch.Functional;

public partial record Recoverable<T, E>
{
    public sealed record Okay(T Value) : Recoverable<T, E>
    {
        public override bool HasValue => true;

        public override bool HasError => false;

        /// <inheritdoc />
        public override Recoverable<TResult, E> And<TResult>(Recoverable<TResult, E> other)
            => other;

        /// <inheritdoc />
        public override Recoverable<TResult, E> AndThen<TResult>(Func<T, Recoverable<TResult, E>> bind)
            => bind(Value);

        /// <inheritdoc />
        public override Recoverable<TResult, E> Select<TResult>(Func<T, TResult> map)
            => new Recoverable<TResult, E>.Okay(map(Value));

        /// <inheritdoc />
        public override Recoverable<T, EResult> SelectError<EResult>(Func<E, EResult> _) => new Recoverable<T, EResult>.Okay(Value);

        /// <inheritdoc />
        public override Recoverable<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> _)
            => new Recoverable<TResult, EResult>.Okay(okay(Value));

        /// <inheritdoc />
        public override Recoverable<TResult, E> Apply<TResult>(Recoverable<Func<T, TResult>, E> function) =>
            function.Select(fn => fn(Value));

        /// <inheritdoc />
        public override Recoverable<T, E> Or(Recoverable<T, E> other) => this;

        /// <inheritdoc />
        public override Recoverable<T, E> OrElse(Func<E, Recoverable<T, E>> _) => this;

        public override string ToString() => $"Okay({Value})";

        public override Recoverable<TResult, E> Zip<TOther, TResult>(Recoverable<TOther, E> other, Func<T, TOther, TResult> zipper)
            => AndThen(FN<T>.Constant(other), zipper);

        public override TResult Match<TResult>(Func<T, TResult> okay, Func<T, E, TResult> warning, Func<E, TResult> fatal) => okay(Value);

        public override T IfFatal(T _) => Value;
    }
}