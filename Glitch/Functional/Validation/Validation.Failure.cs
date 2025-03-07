
using System.Net.Http.Headers;

namespace Glitch.Functional
{
    public static partial class Validation
    {
        public record Failure<T>(Error Error) : Validation<T>
        {
            public override bool IsSuccess => false;

            public override bool IsFailure => true;

            /// <inheritdoc />
            public override Validation<TResult> And<TResult>(Validation<TResult> other)
                => other.Match(
                       _ => new Failure<TResult>(Error),
                       err => new Failure<TResult>(Error + err));

            /// <inheritdoc />
            public override Validation<TResult> AndThen<TResult>(Func<T, Validation<TResult>> bind)
                => new Failure<TResult>(Error);

            /// <inheritdoc />
            public override Validation<TResult> Cast<TResult>() => new Failure<TResult>(Error);

            /// <inheritdoc />
            public override Validation<T> IfOkay(Action<T> _) => this;

            /// <inheritdoc />
            public override Validation<T> IfFail(Action action)
            {
                action();
                return this;
            }

            /// <inheritdoc />
            public override Validation<T> IfFail(Action<Error> action)
            {
                action(Error);
                return this;
            }

            /// <inheritdoc />
            public override IEnumerable<T> Iterate() => [];

            /// <inheritdoc />
            public override Validation<TResult> Map<TResult>(Func<T, TResult> mapper)
                => new Failure<TResult>(Error);

            public override Validation<TResult> MapOr<TResult>(Func<T, TResult> _, Error ifFail)
                => Error + ifFail;

            public override Validation<TResult> MapOrElse<TResult>(Func<T, TResult> _, Func<Error, Error> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Validation<T> MapError(Func<Error, Error> mapper) => mapper(Error);

            /// <inheritdoc />
            public override TResult Match<TResult>(Func<T, TResult> _, Func<Error, TResult> ifFail)
                => ifFail(Error);

            /// <inheritdoc />
            public override Validation<T> Or(Validation<T> other) 
                => other.Match(_ => other, err => new Failure<T>(Error + err));

            /// <inheritdoc />
            public override Validation<T> OrElse(Func<Error, Validation<T>> bindFail) => bindFail(Error);

            /// <inheritdoc />
            public override Option<T> UnwrapOrNone() => None;

            /// <inheritdoc />
            public override Validation<T> Guard(Func<T, bool> predicate, Error _) => this;

            public override Validation<T> Guard(Func<T, bool> predicate, Func<T, Error> _) => this;

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
            public override Validation<TResult> ZipWith<TOther, TResult>(Validation<TOther> other, Func<T, TOther, TResult> _)
            {
                return other.Match(_ => new Failure<TResult>(Error), 
                                   err => new Failure<TResult>(Error + err));
            }

            /// <inheritdoc />
            public override Error UnwrapErrorOr(Error _) => Error;

            /// <inheritdoc />
            public override Error UnwrapErrorOrElse(Func<T, Error> _) => Error;

            /// <inheritdoc />
            public override Option<Error> UnwrapErrorOrNone() => Some(Error);

            /// <inheritdoc />
            public override void ThrowIfFail() => Error.Throw();

            public override Validation<TResult> Choose<TResult>(Func<T, Validation<TResult>> _, Func<Error, Validation<TResult>> ifFail) 
                => ifFail(Error);
        }
    }
}
