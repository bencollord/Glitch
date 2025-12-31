using Glitch.Functional.Validation.Recoverable;

namespace Glitch.Functional;

public partial record Recoverable<T, E>
{
    public sealed record Fatal(E Error) : Recoverable<T, E>
    {
        public override bool HasValue => false;

        public override bool HasError => true;

        /// <inheritdoc />
        public override Recoverable<TResult, E> And<TResult>(Recoverable<TResult, E> other)
            => other.Match(okay: _ => Recoverable.Fatal<TResult, E>(Error),
                           warning: (_, e) => Recoverable.Fatal<TResult, E>(Error),
                           fatal: e => Recoverable.Fatal<TResult, E>(e));

        /// <inheritdoc />
        public override Recoverable<TResult, E> AndThen<TResult>(Func<T, Recoverable<TResult, E>> bind)
            => Recoverable.Fatal<TResult, E>(Error);

        /// <inheritdoc />
        public override Recoverable<TResult, E> Select<TResult>(Func<T, TResult> map)
            => Recoverable.Fatal<TResult, E>(Error);

        /// <inheritdoc />
        public override Recoverable<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => Recoverable.Fatal<T, EResult>(map(Error));

        /// <inheritdoc />
        public override Recoverable<TResult, E> Apply<TResult>(Recoverable<Func<T, TResult>, E> function) =>
            function.Match(okay: _ => Recoverable.Fatal<TResult, E>(Error),
                           warning: (_, e) => Recoverable.Fatal<TResult, E>(Error),
                           fatal: Recoverable.Fatal<TResult, E>);

        /// <inheritdoc />
        public override Recoverable<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> _, Func<E, EResult> error)
            => Recoverable.Fatal<TResult, EResult>(error(Error));

        /// <inheritdoc />
        public override Recoverable<T, E> Or(Recoverable<T, E> other) =>
            other.Match(okay: _ => other,
                        warning: (v, e) => Recoverable.Fatal(e),
                        fatal: e => Recoverable.Fatal(e));

        /// <inheritdoc />
        public override Recoverable<T, E> OrElse(Func<E, Recoverable<T, E>> error) => Or(error(Error));

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> okay, Func<T, E, TResult> warning, Func<E, TResult> fatal) => fatal(Error);

        /// <inheritdoc />
        public override Recoverable<TResult, E> Zip<TOther, TResult>(Recoverable<TOther, E> other, Func<T, TOther, TResult> _) =>
            other.Match(okay: _ => Recoverable.Fatal<TResult, E>(Error),
                        warning: (v, e) => Recoverable.Fatal<TResult, E>(e),
                        fatal: e => Recoverable.Fatal<TResult, E>(e));

        /// <inheritdoc />
        public override string ToString() => $"Error({Error})";

        /// <inheritdoc />
        public override T IfFatal(T fallback) => fallback;
    }
}