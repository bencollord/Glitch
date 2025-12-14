namespace Glitch.Functional.Validation;

public partial record Recoverable<T, E>
{
    public sealed record Warning(T Value, E Error) : Recoverable<T, E>
    {
        public override bool HasValue => true;

        public override bool HasError => true;

        /// <inheritdoc />
        public override Recoverable<TResult, E> And<TResult>(Recoverable<TResult, E> other)
            => other.Match(okay: v => Recoverable.Warning(v, Error),
                           warning: (_, _) => other,
                           fatal: Recoverable.Fatal<TResult, E>);

        /// <inheritdoc />
        public override Recoverable<TResult, E> AndThen<TResult>(Func<T, Recoverable<TResult, E>> bind) => And(bind(Value));

        /// <inheritdoc />
        public override Recoverable<TResult, E> Select<TResult>(Func<T, TResult> map)
            => Recoverable.Warning(map(Value), Error);

        /// <inheritdoc />
        public override Recoverable<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => Recoverable.Warning(Value, map(Error));

        public override Recoverable<TResult, E> Apply<TResult>(Recoverable<Func<T, TResult>, E> function) =>
            function.Match(okay: f => Recoverable.Warning(f(Value), Error),
                           warning: (f, e) => Recoverable.Warning(f(Value), e),
                           fatal: Recoverable.Fatal<TResult, E>);

        /// <inheritdoc />
        public override Recoverable<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> error)
            => Recoverable.Warning(okay(Value), error(Error));

        /// <inheritdoc />
        public override Recoverable<T, E> Or(Recoverable<T, E> other) =>
            other.Match(okay: _ => other,
                        warning: (_, _) => other,
                        fatal: e => new Warning(Value, e));

        /// <inheritdoc />
        public override Recoverable<T, E> OrElse(Func<E, Recoverable<T, E>> fail) => Or(fail(Error));

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> okay, Func<T, E, TResult> warning, Func<E, TResult> fatal) => warning(Value, Error);

        /// <inheritdoc />
        public override Recoverable<TResult, E> Zip<TOther, TResult>(Recoverable<TOther, E> other, Func<T, TOther, TResult> zip) =>
            AndThen(_ => other, zip);

        /// <inheritdoc />
        public override string ToString() => $"Warning({Value}, {Error})";

        public override T IfFatal(T fallback) => Value;
    }
}