namespace Glitch.Functional
{
    public static partial class Result
    {
        public sealed record Success<TOkay, TError>(TOkay Value) : Result<TOkay, TError>
        {
            public override bool IsOkay => true;

            public override bool IsFail => false;

            /// <inheritdoc />
            public override Result<TResult, TError> And<TResult>(Result<TResult, TError> other)
                => other;

            /// <inheritdoc />
            public override Result<TResult, TError> AndThen<TResult>(Func<TOkay, Result<TResult, TError>> mapper)
                => mapper(Value);

            /// <inheritdoc />
            public override Result<TResult, TError> Cast<TResult>()
                => CastOrElse<TResult>(x => throw new InvalidCastException($"Cannot cast value {x} to {typeof(TResult)}"));

            public override Result<TResult, TError> CastOr<TResult>(TError err)
                => CastOrElse<TResult>(_ => err);

            // TODO Clean up syntax here
            public override Result<TResult, TError> CastOrElse<TResult>(Func<TOkay, TError> error)
                => AndThen(v => DynamicCast<TResult>.TryFrom(v).Match<Result<TResult, TError>>(r => new Success<TResult, TError>(r), _ => new Failure<TResult, TError>(error(v))));

            /// <inheritdoc />
            public override Result<TOkay, TError> Do(Action<TOkay> action)
            {
                action(Value);
                return this;
            }

            /// <inheritdoc />
            public override Result<TOkay, TError> IfFail(Action<TError> _) => this;

            /// <inheritdoc />
            public override Result<TOkay, TError> IfError<TDerivedError>(Action<TDerivedError> _) => this;

            /// <inheritdoc />
            public override IEnumerable<TOkay> Iterate()
            {
                yield return Value;
            }

            /// <inheritdoc />
            public override Result<TResult, TError> Map<TResult>(Func<TOkay, TResult> mapper)
                => new Success<TResult, TError>(mapper(Value));

            public override Result<TResult, TError> MapOr<TResult>(Func<TOkay, TResult> mapper, TError _)
                => Map(mapper);

            public override Result<TResult, TError> MapOrElse<TResult>(Func<TOkay, TResult> ifOkay, Func<TError, TError> _)
                => Map(ifOkay);

            /// <inheritdoc />
            public override Result<TOkay, TNewError> MapError<TNewError>(Func<TError, TNewError> _) => Result<TOkay, TNewError>.Okay(Value);

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<TOkay, TResult> ifOkay, Func<TError, TResult> _)
                => ifOkay(Value);

            /// <inheritdoc />
            public override Result<TOkay, TError> Or(Result<TOkay, TError> other) => this;

            /// <inheritdoc />
            public override Result<TOkay, TError> OrElse(Func<TError, Result<TOkay, TError>> _) => this;

            /// <inheritdoc />
            public override Result<TOkay, TError> Guard(Func<TOkay, bool> predicate, TError error)
                => predicate(Value) ? this : error;

            public override Result<TOkay, TError> Guard(Func<TOkay, bool> predicate, Func<TOkay, TError> error)
                => predicate(Value) ? this : error(Value);

            /// <inheritdoc />
            public override Option<TOkay> OkayOrNone() => Some(Value);

            public override string ToString() => $"Ok: {Value}";

            /// <inheritdoc />
            public override TOkay Unwrap() => Value;

            /// <inheritdoc />
            public override TOkay IfFail(TOkay _) => Value;

            /// <inheritdoc />
            public override TOkay IfFail(Func<TOkay> _) => Value;

            /// <inheritdoc />
            public override TOkay IfFail(Func<TError, TOkay> _) => Value;

            public override TError UnwrapErrorOr(TError fallback) => fallback;

            public override Result<TResult, TError> Choose<TResult>(Func<TOkay, Result<TResult, TError>> ifOkay, Func<TError, Result<TResult, TError>> _)
            {
                return ifOkay(Value);
            }

            public override bool TryUnwrap(out TOkay result)
            {
                result = Value;
                return true;
            }

            public override bool TryUnwrapError(out TError result)
            {
                result = default!;
                return false;
            }

            public override TError UnwrapErrorOrElse(Func<TOkay, TError> fallback) => fallback(Value);

            public override Option<TError> ErrorOrNone() => None;
        }
    }
}