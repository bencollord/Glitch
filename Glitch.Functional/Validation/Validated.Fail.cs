using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    // UNDONE Need to figure out how we actually want to accumulate errors here.
    public sealed record Fail(Sequence<E> Errors) : Validated<T, E>
    {
        public Fail(params IEnumerable<E> errors) : this(Sequence.From(errors)) { }

        public override bool IsOkay => false;

        public override bool IsFail => true;

        /// <inheritdoc />
        public override Validated<TResult, E> And<TResult>(Validated<TResult, E> other)
            => new Validated<TResult, E>.Fail(Errors);

        /// <inheritdoc />
        public override Validated<TResult, E> AndThen<TResult>(Func<T, Validated<TResult, E>> mapper)
            => new Validated<TResult, E>.Fail(Errors);

        /// <inheritdoc />
        public override Validated<TResult, E> Select<TResult>(Func<T, TResult> map)
            => new Validated<TResult, E>.Fail(Errors);

        /// <inheritdoc />
        public override Validated<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => new Validated<T, EResult>.Fail(Errors.Select(map));

        /// <inheritdoc />
        public override Validated<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> _, Func<E, EResult> fail)
            => new Validated<TResult, EResult>.Fail(Errors.Select(fail));

        /// <inheritdoc />
        public override Validated<T, EResult> Or<EResult>(Validated<T, EResult> other) => other;

        /// <inheritdoc />
        public override Validated<T, EResult> OrElse<EResult>(Func<Sequence<E>, Validated<T, EResult>> fail) => fail(Errors);

        /// <inheritdoc />
        public override Validated<T, E> Guard(Func<T, bool> predicate, E _) => this;

        public override Validated<T, E> Guard(Func<T, bool> predicate, Func<T, E> _) => this;

        public override TResult Match<TResult>(Func<T, TResult> okay, Func<Sequence<E>, TResult> fail)
        {
            throw new NotImplementedException();
        }

        public override Validated<TResult, E> Zip<TOther, TResult>(Validated<TOther, E> other, Func<T, TOther, TResult> zipper)
        {
            throw new NotImplementedException();
        }
        public override string ToString() => $"Error({Errors})";
    }
}