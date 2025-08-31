using Glitch.Functional.Results;

namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        public sealed record Success<T, E>(T Value) : Expected<T, E>
        {
            public override bool IsOkay => true;

            public override bool IsError => false;

            /// <inheritdoc />
            public override Expected<TResult, E> And<TResult>(Expected<TResult, E> other)
                => other;

            /// <inheritdoc />
            public override Expected<TResult, E> AndThen<TResult>(Func<T, Expected<TResult, E>> mapper)
                => mapper(Value);

            /// <inheritdoc />
            public override Expected<T, E> IfFail(Action<E> _) => this;

            /// <inheritdoc />
            public override IEnumerable<T> Iterate()
            {
                yield return Value;
            }

            /// <inheritdoc />
            public override Expected<TResult, E> Select<TResult>(Func<T, TResult> mapper)
                => new Success<TResult, E>(mapper(Value));

            /// <inheritdoc />
            public override Expected<T, TNewError> SelectError<TNewError>(Func<E, TNewError> _) => Expected<T, TNewError>.Okay(Value);

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<T, TResult> ifOkay, Func<E, TResult> _)
                => ifOkay(Value);

            /// <inheritdoc />
            public override Expected<T, EResult> Or<EResult>(Expected<T, EResult> other) => Okay<T, EResult>(Value);

            /// <inheritdoc />
            public override Expected<T, EResult> OrElse<EResult>(Func<E, Expected<T, EResult>> _) => Okay<T, EResult>(Value);

            /// <inheritdoc />
            public override Expected<T, E> Guard(Func<T, bool> predicate, E error)
                => predicate(Value) ? this : error;

            public override Expected<T, E> Guard(Func<T, bool> predicate, Func<T, E> error)
                => predicate(Value) ? this : error(Value);

            /// <inheritdoc />
            public override Option<T> OkayOrNone() => Option.Some(Value);

            public override string ToString() => $"Ok: {Value}";

            /// <inheritdoc />
            public override T Unwrap() => Value;

            /// <inheritdoc />
            public override T IfFail(T _) => Value;

            /// <inheritdoc />
            public override T IfFail(Func<E, T> _) => Value;

            public override E UnwrapErrorOr(E fallback) => fallback;

            public override bool TryUnwrap(out T result)
            {
                result = Value;
                return true;
            }

            public override bool TryUnwrapError(out E result)
            {
                result = default!;
                return false;
            }

            public override E UnwrapErrorOrElse(Func<T, E> fallback) => fallback(Value);

            public override Option<E> ErrorOrNone() => Option.None;
        }
    }
}