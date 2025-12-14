using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    public sealed record Fatal(Sequence<E> Errors) : Validated<T, E>
    {
        public Fatal(params IEnumerable<E> errors) : this(Sequence.From(errors)) { }

        public override bool HasValue => false;

        public override bool HasError => true;

        /// <inheritdoc />
        public override Validated<TResult, E> And<TResult>(Validated<TResult, E> other)
            => other.Match(okay: _ => new Validated<TResult, E>.Fatal(Errors),
                           warning: (_, e) => new Validated<TResult, E>.Fatal(Errors.Concat(e)),
                           fatal: e => new Validated<TResult, E>.Fatal(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<TResult, E> AndThen<TResult>(Func<T, Validated<TResult, E>> bind)
            => new Validated<TResult, E>.Fatal(Errors);

        /// <inheritdoc />
        public override Validated<TResult, E> Select<TResult>(Func<T, TResult> map)
            => new Validated<TResult, E>.Fatal(Errors);

        /// <inheritdoc />
        public override Validated<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => new Validated<T, EResult>.Fatal(Errors.Select(map));

        /// <inheritdoc />
        public override Validated<TResult, E> Apply<TResult>(Validated<Func<T, TResult>, E> function) =>
            function.Match(okay: _ => new Validated<TResult, E>.Fatal(Errors),
                           warning: (_, e) => new Validated<TResult, E>.Fatal(Errors.Concat(e)),
                           fatal: e => new Validated<TResult, E>.Fatal(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> _, Func<E, EResult> fail)
            => new Validated<TResult, EResult>.Fatal(Errors.Select(fail));

        /// <inheritdoc />
        public override Validated<T, E> Or(Validated<T, E> other) =>
            // DESIGN Should this actually just return other on failure? I'll have to play with this to decide.
            other.Match(okay: _ => other,
                        warning: (v, e) => new Fatal(Errors.Concat(e)),
                        fatal: e => new Fatal(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<T, E> OrElse(Func<Sequence<E>, Validated<T, E>> fail) => Or(fail(Errors));

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> okay, Func<T, Sequence<E>, TResult> warning, Func<Sequence<E>, TResult> fatal) => fatal(Errors);

        /// <inheritdoc />
        public override Validated<TResult, E> Zip<TOther, TResult>(Validated<TOther, E> other, Func<T, TOther, TResult> _) =>
            other.Match(okay: _ => new Validated<TResult, E>.Fatal(Errors),
                        warning: (v, e) => new Validated<TResult, E>.Fatal(Errors.Concat(e)),
                        fatal: e => new Validated<TResult, E>.Fatal(Errors.Concat(e)));

        /// <inheritdoc />
        public override string ToString() => $"Error({Errors.Join(", ")})";

        /// <inheritdoc />
        public override T IfFatal(T fallback) => fallback;
    }
}