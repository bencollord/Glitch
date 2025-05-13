
namespace Glitch.Functional
{
    public static partial class Result
    {
        public sealed record Fail<T>(Error Error) : Result<T>
        {
            public override bool IsOkay => false;

            public override bool IsFail => true;

            /// <inheritdoc />
            public override Result<TResult> And<TResult>(Result<TResult> other)
                => new Fail<TResult>(Error);

            /// <inheritdoc />
            public override Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> mapper)
                => new Fail<TResult>(Error);

            /// <inheritdoc />
            public override Result<TResult> Cast<TResult>() => new Fail<TResult>(Error);

            public override Result<TResult> CastOr<TResult>(Error _) => Cast<TResult>();

            public override Result<TResult> CastOrElse<TResult>(Func<T, Error> _) => Cast<TResult>();

            /// <inheritdoc />
            public override Result<T> IfOkay(Action<T> _) => this;

            /// <inheritdoc />
            public override Result<T> IfFail(Action<Error> action)
            {
                action(Error);
                return this;
            }

            /// <inheritdoc />
            public override Result<T> IfError<TError>(Action<TError> action)
            {
                if (Error is TError derived)
                {
                    action(derived);
                }

                return this;
            }

            /// <inheritdoc />
            public override IEnumerable<T> Iterate() => Enumerable.Empty<T>();

            /// <inheritdoc />
            public override Result<TResult> Map<TResult>(Func<T, TResult> mapper)
                => new Fail<TResult>(Error);

            public override Result<TResult> MapOr<TResult>(Func<T, TResult> _, Error ifFail)
                => ifFail;

            public override Result<TResult> MapOrElse<TResult>(Func<T, TResult> _, Func<Error, Error> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Result<T> MapError(Func<Error, Error> mapper) => mapper(Error);

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<T, TResult> _, Func<Error, TResult> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Result<T> Or(Result<T> other) => other;

            /// <inheritdoc />
            public override Result<T> OrElse(Func<Error, Result<T>> bindFail) => bindFail(Error);

            /// <inheritdoc />
            public override Option<T> OrNone() => None;

            /// <inheritdoc />
            public override Result<T> Guard(Func<T, bool> predicate, Error _) => this;

            public override Result<T> Guard(Func<T, bool> predicate, Func<T, Error> _) => this;

            public override string ToString() => $"Error: {Error}";

            /// <inheritdoc />
            public override T Unwrap() => Error.Throw<T>();

            public override bool TryUnwrap(out T result)
            {
                result = default!;
                return false;
            }

            public override bool TryUnwrapError(out Error result)
            {
                result = Error;
                return true;
            }

            /// <inheritdoc />
            public override T IfFail(T fallback) => fallback;

            /// <inheritdoc />
            public override T IfFail(Func<T> fallback) => fallback();

            /// <inheritdoc />
            public override T IfFail(Func<Error, T> fallback) => fallback(Error);

            public override bool IsOkayAnd(Func<T, bool> _) => false;

            public override bool IsFailAnd(Func<Error, bool> predicate) => predicate(Error);

            /// <inheritdoc />
            public override Result<TResult> Zip<TOther, TResult>(Result<TOther> other, Func<T, TOther, TResult> _)
            {
                return other.MapError(err => Error.New(Error, err))
                            .AndThen(_ => Fail<TResult>(Error));
            }

            /// <inheritdoc />
            public override Error UnwrapErrorOr(Error _) => Error;

            /// <inheritdoc />
            public override Error UnwrapErrorOrElse(Func<T, Error> _) => Error;

            /// <inheritdoc />
            public override Option<Error> ErrorOrNone() => Some(Error);

            /// <inheritdoc />
            public override void ThrowIfFail() => Error.Throw();

            public override Result<TResult> Choose<TResult>(Func<T, Result<TResult>> _, Func<Error, Result<TResult>> ifFail) 
                => ifFail(Error);
        }
    }
}
