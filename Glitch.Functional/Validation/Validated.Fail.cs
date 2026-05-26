using System.Collections.Immutable;

namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    public sealed record Fail(ImmutableList<E> Errors) : Validated<T, E>
    {
        public Fail(params IEnumerable<E> errors) : this([.. errors]) { }

        public override bool IsOkay => false;

        public override bool IsFail => true;

        /// <inheritdoc />
        public override Validated<TResult, E> And<TResult>(Validated<TResult, E> other)
            => other.Match(okay: _ => new Validated<TResult, E>.Fail(Errors),
                           fail: e => new Validated<TResult, E>.Fail(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<TResult, E> AndThen<TResult>(Func<T, Validated<TResult, E>> bind)
            => new Validated<TResult, E>.Fail(Errors);

        /// <inheritdoc />
        public override Validated<TResult, E> Select<TResult>(Func<T, TResult> map)
            => new Validated<TResult, E>.Fail(Errors);

        /// <inheritdoc />
        public override Validated<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => new Validated<T, EResult>.Fail(Errors.Select(map));

        public override Validated<T, EResult> SelectErrors<EResult>(Func<ImmutableList<E>, EResult> map) =>
            new Validated<T, EResult>.Fail([map(Errors)]);

        /// <inheritdoc />
        public override Validated<TResult, E> Apply<TResult>(Validated<Func<T, TResult>, E> function) =>
            function.Match(okay: _ => new Validated<TResult, E>.Fail(Errors),
                           fail: e => new Validated<TResult, E>.Fail(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> _, Func<E, EResult> fail)
            => new Validated<TResult, EResult>.Fail(Errors.Select(fail));

        /// <inheritdoc />
        public override Validated<T, E> Or(Validated<T, E> other) =>
            other.Match(okay: _ => other,
                        fail: e => new Fail(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<T, E> OrElse(Func<ImmutableList<E>, Validated<T, E>> fail) => Or(fail(Errors));

        /// <inheritdoc />
        public override Validated<T, E> Guard(Func<T, bool> predicate, E _) => this;

        /// <inheritdoc />
        public override Validated<T, E> Guard(Func<T, bool> predicate, Func<T, E> _) => this;

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> okay, Func<ImmutableList<E>, TResult> fail) => fail(Errors);

        /// <inheritdoc />
        public override Validated<TResult, E> Zip<TOther, TResult>(Validated<TOther, E> other, Func<T, TOther, TResult> _) =>
            other.Match(okay: _ => new Validated<TResult, E>.Fail(Errors),
                        fail: e => new Validated<TResult, E>.Fail(Errors.Concat(e)));

        /// <inheritdoc />
        public override string ToString() => $"Error({Errors.Join(", ")})";

        public override T Unwrap() =>
            throw Errors.Select(Option.Maybe)
                  .Select(e => e.AndThen(x => Option.Maybe(x.ToString())))
                  .Somes()
                  .PipeInto(x => new ValidationException(x));
    }
}