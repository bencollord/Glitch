
namespace Glitch.Functional
{
    public static partial class Result
    {
        public record Fail<T>(Error Error) : Result<T>
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

            /// <inheritdoc />
            public override Result<T> IfOkay(Action<T> _) => this;

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
                => new Fail<TResult>(Error);

            public override Result<TResult> MapOr<TResult>(Func<T, TResult> _, TResult ifFail)
                => ifFail;

            public override Result<TResult> MapOrElse<TResult>(Func<T, TResult> _, Func<Error, TResult> ifFail) 
                => ifFail(Error);

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
            public override Result<T> OrElse(Func<Error, Result<T>> ifFail) => ifFail(Error);

            /// <inheritdoc />
            public override Option<T> UnwrapOrNone() => None;

            /// <inheritdoc />
            public override Result<T> Filter(Func<T, bool> predicate) => this;

            public override string ToString() => $"Error: {Error}";

            /// <inheritdoc />
            public override T Unwrap() => Error.Throw<T>();

            /// <inheritdoc />
            public override T IfFail(T fallback) => fallback;

            /// <inheritdoc />
            public override T IfFail(Func<T> fallback) => fallback();

            /// <inheritdoc />
            public override T IfFail(Func<Error, T> fallback) => fallback(Error);

            public override bool IsOkayAnd(Func<T, bool> _) => false;

            public override bool IsFailAnd(Func<Error, bool> predicate) => predicate(Error);

            /// <inheritdoc />
            public override Fallible<TResult> Try<TResult>(Func<T, TResult> map) 
                => Functional.Fallible<TResult>.Fail(Error);

            /// <inheritdoc />
            public override Fallible<TResult> AndThenTry<TResult>(Func<T, Result<TResult>> bind)
                => Functional.Fallible<TResult>.Fail(Error);

            /// <inheritdoc />
            public override Fallible<TResult> AndThenTry<TResult>(Func<T, Fallible<TResult>> bind)
                => Functional.Fallible<TResult>.Fail(Error);

            /// <inheritdoc />
            public override Result<TResult> ZipWith<TOther, TResult>(Result<TOther> other, Func<T, TOther, TResult> _)
            {
                return other.MapError(err => Error.New(Error, err))
                            .AndThen(_ => Fail<TResult>(Error));
            }

            /// <inheritdoc />
            public override Error UnwrapErrorOr(Error _) => Error;

            /// <inheritdoc />
            public override Error UnwrapErrorOrElse(Func<T, Error> _) => Error;

            /// <inheritdoc />
            public override Option<Error> UnwrapErrorOrNone() => Some(Error);

            /// <inheritdoc />
            public override void ThrowIfFail() => Error.Throw();

            public override Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> _, Func<Error, Result<TResult>> ifFail) 
                => ifFail(Error);
        }
    }
}
