
namespace Glitch.Functional
{
    public abstract partial class Result<T>
    {
        public class Fail : Result<T>
        {
            public Fail(Error error)
            {
                Error = error;
            }

            public Error Error { get; }

            public override bool IsOk => false;

            public override bool IsFail => true;

            /// <inheritdoc />
            public override Result<TResult> And<TResult>(Result<TResult> other)
                => new Result<TResult>.Fail(Error);

            /// <inheritdoc />
            public override Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> mapper)
                => new Result<TResult>.Fail(Error);

            /// <inheritdoc />
            public override Result<TResult> Cast<TResult>() => new Result<TResult>.Fail(Error);

            public override bool Equals(Result<T>? other)
            {
                if (other is null) return false;
                if (ReferenceEquals(this, other)) return true;

                if (other is Fail fail)
                {
                    return Error.Equals(fail.Error);
                }

                return false;
            }

            public override int GetHashCode() => Error.GetHashCode();

            /// <inheritdoc />
            public override Result<T> Do(Action<T> _) => this;

            /// <inheritdoc />
            public override Result<T> IfFail(Action action)
            {
                action();
                return this;
            }

            /// <inheritdoc />
            public override Result<T> IfFail(Action<Error> action)
            {
                action(Error);
                return this;
            }

            /// <inheritdoc />
            public override IEnumerable<T> Iterate() => Enumerable.Empty<T>();

            /// <inheritdoc />
            public override Result<TResult> Map<TResult>(Func<T, TResult> mapper)
                => new Result<TResult>.Fail(Error);

            /// <inheritdoc />
            public override Result<T> MapError(Func<Error, Error> mapper) => mapper(Error);

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<T, TResult> _, Func<Error, TResult> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Result<T> Or(Result<T> other) => other;

            /// <inheritdoc />
            public override Result<T> OrElse(Func<Error, Result<T>> ifFail) => ifFail(Error);

            /// <inheritdoc />
            public override Option<T> ToOption() => Option.None;

            /// <inheritdoc />
            public override Result<T> Filter(Func<T, bool> predicate) => this;

            public override string ToString() => $"Error: {Error}";

            /// <inheritdoc />
            public override T Unwrap() => Error.Throw<T>();

            /// <inheritdoc />
            public override T UnwrapOr(T fallback) => fallback;

            /// <inheritdoc />
            public override T UnwrapOrElse(Func<T> fallback) => fallback();

            /// <inheritdoc />
            public override T UnwrapOrElse(Func<Error, T> fallback) => fallback(Error);

            public override bool IsOkAnd(Func<T, bool> _) => false;

            public override bool IsFailAnd(Func<Error, bool> predicate) => predicate(Error);
        }
    }
}
