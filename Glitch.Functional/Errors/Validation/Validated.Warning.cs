using Glitch.Functional.Collections;
using System.Diagnostics;

namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    public sealed record Warning(T Value, Sequence<E> Errors) : Validated<T, E>
    {
        public Warning(T value, params IEnumerable<E> errors) : this(value, Sequence.From(errors)) { }

        public override bool HasValue => true;

        public override bool HasError => true;

        /// <inheritdoc />
        public override Validated<TResult, E> And<TResult>(Validated<TResult, E> other)
            => other.Match(okay: v => new Validated<TResult, E>.Warning(v, Errors) as Validated<TResult, E>,
                           warning: (v, e) => new Validated<TResult, E>.Warning(v, Errors.Concat(e)),
                           fatal: e => new Validated<TResult, E>.Fatal(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<TResult, E> AndThen<TResult>(Func<T, Validated<TResult, E>> bind) => And(bind(Value));

        /// <inheritdoc />
        public override Validated<TResult, E> Select<TResult>(Func<T, TResult> map)
            => new Validated<TResult, E>.Warning(map(Value), Errors);

        /// <inheritdoc />
        public override Validated<T, EResult> SelectError<EResult>(Func<E, EResult> map)
            => new Validated<T, EResult>.Warning(Value, Errors.Select(map));

        public override Validated<TResult, E> Apply<TResult>(Validated<Func<T, TResult>, E> function) =>
            function.Match(okay: f => new Validated<TResult, E>.Warning(f(Value), Errors) as Validated<TResult, E>,
                           warning: (f, e) => new Validated<TResult, E>.Warning(f(Value), Errors.Concat(e)),
                           fatal: e => new Validated<TResult, E>.Fatal(Errors.Concat(e)));

        /// <inheritdoc />
        public override Validated<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> error)
            => new Validated<TResult, EResult>.Warning(okay(Value), Errors.Select(error));

        /// <inheritdoc />
        public override Validated<T, E> Or(Validated<T, E> other) =>
            other.Match(okay: _ => other,
                        warning: (v, e) => new Warning(v, Errors.Concat(e)),
                        fatal: e => new Warning(Value, Errors.Concat(e))); // UNDONE Should this just return other? That's how the ChronicleT type in LangaugeExt does it. Need to play with it more and decide what to do.

        /// <inheritdoc />
        public override Validated<T, E> OrElse(Func<Sequence<E>, Validated<T, E>> fail) => Or(fail(Errors));

        /// <inheritdoc />
        public override TResult Match<TResult>(Func<T, TResult> okay, Func<T, Sequence<E>, TResult> warning, Func<Sequence<E>, TResult> fatal) => warning(Value, Errors);

        /// <inheritdoc />
        public override Validated<TResult, E> Zip<TOther, TResult>(Validated<TOther, E> other, Func<T, TOther, TResult> zip) =>
            other.Match(okay: v => new Validated<TResult, E>.Warning(zip(Value, v), Errors) as Validated<TResult, E>,
                        warning: (v, e) => new Validated<TResult, E>.Warning(zip(Value, v), Errors.Concat(e)),
                        fatal: e => new Validated<TResult, E>.Fatal(Errors.Concat(e)));

        /// <inheritdoc />
        public override string ToString() => $"Error({Errors.Join(", ")})";

        public override T IfFatal(T fallback) => Value;
    }
}