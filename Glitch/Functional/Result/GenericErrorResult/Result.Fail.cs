
namespace Glitch.Functional
{
    public static partial class Result
    {
        public sealed record Fail<TOkay, TError>(TError Error) : Result<TOkay, TError>
        {
            public override bool IsOkay => false;

            public override bool IsFail => true;

            /// <inheritdoc />
            public override Result<TResult, TError> And<TResult>(Result<TResult, TError> other)
                => new Fail<TResult, TError>(Error);

            /// <inheritdoc />
            public override Result<TResult, TError> AndThen<TResult>(Func<TOkay, Result<TResult, TError>> mapper)
                => new Fail<TResult, TError>(Error);

            /// <inheritdoc />
            public override Result<TResult, TError> Cast<TResult>() => new Fail<TResult, TError>(Error);

            public override Result<TResult, TError> CastOr<TResult>(TError _) => Cast<TResult>();

            public override Result<TResult, TError> CastOrElse<TResult>(Func<TOkay, TError> _) => Cast<TResult>();

            /// <inheritdoc />
            public override Result<TOkay, TError> Do(Action<TOkay> _) => this;

            /// <inheritdoc />
            public override Result<TOkay, TError> IfFail(Action<TError> action)
            {
                action(Error);
                return this;
            }

            /// <inheritdoc />
            public override Result<TOkay, TError> IfError<TDerivedError>(Action<TDerivedError> action)
            {
                if (Error is TDerivedError derived)
                {
                    action(derived);
                }

                return this;
            }

            /// <inheritdoc />
            public override IEnumerable<TOkay> Iterate() => Enumerable.Empty<TOkay>();

            /// <inheritdoc />
            public override Result<TResult, TError> Map<TResult>(Func<TOkay, TResult> mapper)
                => new Fail<TResult, TError>(Error);

            public override Result<TResult, TError> MapOr<TResult>(Func<TOkay, TResult> _, TError ifFail)
                => ifFail;

            public override Result<TResult, TError> MapOrElse<TResult>(Func<TOkay, TResult> _, Func<TError, TError> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Result<TOkay, TError> MapError(Func<TError, TError> mapper) => mapper(Error);

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<TOkay, TResult> _, Func<TError, TResult> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Result<TOkay, TError> Or(Result<TOkay, TError> other) => other;

            /// <inheritdoc />
            public override Result<TOkay, TError> OrElse(Func<TError, Result<TOkay, TError>> bindFail) => bindFail(Error);

            /// <inheritdoc />
            public override Option<TOkay> OrNone() => None;

            /// <inheritdoc />
            public override Result<TOkay, TError> Guard(Func<TOkay, bool> predicate, TError _) => this;

            public override Result<TOkay, TError> Guard(Func<TOkay, bool> predicate, Func<TOkay, TError> _) => this;

            public override string ToString() => $"Error: {Error}";

            /// <inheritdoc />
            public override TOkay Unwrap() => throw new InvalidOperationException($"Attempted to unwrap a faulted result. Error value: {Error}");

            public override bool TryUnwrap(out TOkay result)
            {
                result = default!;
                return false;
            }

            public override bool TryUnwrapError(out TError result)
            {
                result = Error;
                return true;
            }

            /// <inheritdoc />
            public override TOkay IfFail(TOkay fallback) => fallback;

            /// <inheritdoc />
            public override TOkay IfFail(Func<TOkay> fallback) => fallback();

            /// <inheritdoc />
            public override TOkay IfFail(Func<TError, TOkay> fallback) => fallback(Error);

            public override bool IsOkayAnd(Func<TOkay, bool> _) => false;

            public override bool IsFailAnd(Func<TError, bool> predicate) => predicate(Error);

            /// <inheritdoc />
            public override TError UnwrapErrorOr(TError _) => Error;

            /// <inheritdoc />
            public override TError UnwrapErrorOrElse(Func<TOkay, TError> _) => Error;

            /// <inheritdoc />
            public override Option<TError> ErrorOrNone() => Some(Error);

            public override Result<TResult, TError> Choose<TResult>(Func<TOkay, Result<TResult, TError>> _, Func<TError, Result<TResult, TError>> ifFail) => ifFail(Error);
        }
    }
}