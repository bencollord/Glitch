
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public abstract partial class Result<T>
    {
        public class Okay : Result<T>
        {
            public Okay(T value)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
            }

            [NotNull]
            public T Value { get; }

            public override bool IsOk => true;

            public override bool IsFail => false;

            /// <inheritdoc />
            public override Result<TResult> And<TResult>(Result<TResult> other)
                => other;

            /// <inheritdoc />
            public override Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> mapper)
                => mapper(Value);

            /// <inheritdoc />
            public override Result<TResult> Cast<TResult>() 
                => (TResult)(dynamic)Value!;

            public override bool Equals(Result<T>? other)
            {
                if (other is null) return false;
                if (ReferenceEquals(this, other)) return true;

                if (other is Okay ok)
                {
                    return Value.Equals(ok.Value);
                }

                return false;
            }

            public override int GetHashCode() => Value.GetHashCode();

            /// <inheritdoc />
            public override Result<T> Do(Action<T> action)
            {
                action(Value);
                return this;
            }

            /// <inheritdoc />
            public override Result<T> IfFail(Action _) => this;

            /// <inheritdoc />
            public override Result<T> IfFail(Action<Error> _) => this;

            /// <inheritdoc />
            public override IEnumerable<T> Iterate()
            {
                yield return Value;
            }

            /// <inheritdoc />
            public override Result<TResult> Map<TResult>(Func<T, TResult> mapper)
                => new Result<TResult>.Okay(mapper(Value));

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
            public override Result<T> Filter(Func<T, bool> predicate)
                => predicate(Value) ? this : Fail<T>(new ApplicationError("Result failed check"));

            /// <inheritdoc />
            public override Option<T> ToOption() => Option.Some(Value);

            public override string ToString() => $"Ok: {Value}";

            /// <inheritdoc />
            public override T Unwrap() => Value;

            /// <inheritdoc />
            public override T UnwrapOr(T _) => Value;

            /// <inheritdoc />
            public override T UnwrapOrElse(Func<T> _) => Value;

            /// <inheritdoc />
            public override T UnwrapOrElse(Func<Error, T> _) => Value;

            public override bool IsOkAnd(Func<T, bool> predicate) => predicate(Value);

            public override bool IsFailAnd(Func<Error, bool> _) => false;
        }
    }
}
