namespace Glitch.Functional
{
    public static partial class Result
    {
        public sealed record Okay<T>(T Value) : Result<T>
        {
            public override bool IsOkay => true;

            public override bool IsFail => false;

            /// <inheritdoc />
            public override Result<TResult> And<TResult>(Result<TResult> other)
                => other;

            /// <inheritdoc />
            public override Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> mapper)
                => mapper(Value);

            /// <inheritdoc />
            public override Result<TResult> Cast<TResult>()
                => Fallible.Lift(this).Cast<TResult>().Run();

            public override Result<TResult> CastOr<TResult>(Error err)
                => Fallible.Lift(this).CastOr<TResult>(err).Run();

            public override Result<TResult> CastOrElse<TResult>(Func<T, Error> error)
                => Fallible.Lift(this).CastOrElse<TResult>(error).Run();

            /// <inheritdoc />
            public override Result<T> IfOkay(Action<T> action)
            {
                action(Value);
                return this;
            }

            /// <inheritdoc />
            public override Result<T> IfFail(Action<Error> _) => this;

            /// <inheritdoc />
            public override Result<T> IfError<TError>(Action<TError> _) => this;

            /// <inheritdoc />
            public override IEnumerable<T> Iterate()
            {
                yield return Value;
            }

            /// <inheritdoc />
            public override Result<TResult> Map<TResult>(Func<T, TResult> mapper)
                => new Okay<TResult>(mapper(Value));

            public override Result<TResult> MapOr<TResult>(Func<T, TResult> mapper, Error _)
                => Map(mapper);

            public override Result<TResult> MapOrElse<TResult>(Func<T, TResult> ifOkay, Func<Error, Error> _)
                => Map(ifOkay);

            /// <inheritdoc />
            public override Result<T> MapError(Func<Error, Error> _) => this;

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> _)
                => ifOkay(Value);

            /// <inheritdoc />
            public override Result<T> Or(Result<T> other) => this;

            /// <inheritdoc />
            public override Result<T> OrElse(Func<Error, Result<T>> _) => this;

            /// <inheritdoc />
            public override Result<T> Guard(Func<T, bool> predicate, Error error)
                => predicate(Value) ? this : error;

            public override Result<T> Guard(Func<T, bool> predicate, Func<T, Error> error)
                => predicate(Value) ? this : error(Value);

            /// <inheritdoc />
            public override Option<T> OrNone() => Some(Value);

            public override string ToString() => $"Ok: {Value}";

            /// <inheritdoc />
            public override T Unwrap() => Value;

            /// <inheritdoc />
            public override T IfFail(T _) => Value;

            /// <inheritdoc />
            public override T IfFail(Func<T> _) => Value;

            /// <inheritdoc />
            public override T IfFail(Func<Error, T> _) => Value;

            public override bool IsOkayAnd(Func<T, bool> predicate) => predicate(Value);

            public override bool IsFailAnd(Func<Error, bool> _) => false;

            public override Result<TResult> Zip<TOther, TResult>(Result<TOther> other, Func<T, TOther, TResult> zipper) 
                => other.Map(val => zipper(Value, val));

            public override Error UnwrapErrorOr(Error fallback) => fallback;

            public override Error UnwrapErrorOrElse(Func<T, Error> fallback) => fallback(Value);

            public override bool TryUnwrap(out T result)
            {
                result = Value;
                return true;
            }

            public override bool TryUnwrapError(out Error result)
            {
                result = default!;
                return false;
            }

            public override Option<Error> ErrorOrNone() => None;

            /// <inheritdoc />
            public override void ThrowIfFail()
            {
                // Nop
            }

            public override Result<TResult> Choose<TResult>(Func<T, Result<TResult>> ifOkay, Func<Error, Result<TResult>> _) => AndThen(ifOkay);

            public static implicit operator T(Okay<T> ok) => ok.Value;
        }
    }
}
