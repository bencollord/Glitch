
using Glitch.Functional.Results;

namespace Glitch.Functional.Results
{
    using static Option;

    public static partial class Result
    {
        public sealed record Failure<T, E>(E Error) : Result<T, E>
        {
            public override bool IsOkay => false;

            public override bool IsError => true;

            /// <inheritdoc />
            public override Result<TResult, E> And<TResult>(Result<TResult, E> other)
                => new Failure<TResult, E>(Error);

            /// <inheritdoc />
            public override Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> mapper)
                => new Failure<TResult, E>(Error);

            /// <inheritdoc />
            public override Result<T, E> IfFail(Action<E> action)
            {
                action(Error);
                return this;
            }

            /// <inheritdoc />
            public override IEnumerable<T> Iterate() => Enumerable.Empty<T>();

            /// <inheritdoc />
            public override Result<TResult, E> Select<TResult>(Func<T, TResult> mapper)
                => new Failure<TResult, E>(Error);

            /// <inheritdoc />
            public override Result<T, TNewError> SelectError<TNewError>(Func<E, TNewError> mapper) => mapper(Error);

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<T, TResult> _, Func<E, TResult> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Result<T, EResult> Or<EResult>(Result<T, EResult> other) => other;

            /// <inheritdoc />
            public override Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> bindFail) => bindFail(Error);

            /// <inheritdoc />
            public override Option<T> OkayOrNone() => None;

            /// <inheritdoc />
            public override Result<T, E> Guard(Func<T, bool> predicate, E _) => this;

            public override Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> _) => this;

            public override string ToString() => $"Error: {Error}";

            /// <inheritdoc />
            public override T Unwrap() => throw new InvalidOperationException($"Attempted to unwrap a faulted result. Error value: {Error}");

            public override bool TryUnwrap(out T result)
            {
                result = default!;
                return false;
            }

            public override bool TryUnwrapError(out E result)
            {
                result = Error;
                return true;
            }

            /// <inheritdoc />
            public override T IfFail(T fallback) => fallback;

            /// <inheritdoc />
            public override T IfFail(Func<E, T> fallback) => fallback(Error);

            /// <inheritdoc />
            public override E UnwrapErrorOr(E _) => Error;

            /// <inheritdoc />
            public override E UnwrapErrorOrElse(Func<T, E> _) => Error;

            /// <inheritdoc />
            public override Option<E> ErrorOrNone() => Some(Error);
        }
    }
}