
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public abstract partial record Result<T>
    {
        public record Okay : Result<T>
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

            public override Result<TResult> MapOr<TResult>(Func<T, TResult> mapper, TResult _) 
                => Map(mapper);

            public override Result<TResult> MapOrElse<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> _) 
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
            public override Result<T> Filter(Func<T, bool> predicate)
                => predicate(Value) ? this : Fail<T>(new ApplicationError("Result failed check"));

            /// <inheritdoc />
            public override Option<T> UnwrapOrNone() => Option.Some(Value);

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

            /// <inheritdoc />
            public override Try<TResult> Try<TResult>(Func<T, TResult> map) 
                => FN.Try(() => Map(map));

            /// <inheritdoc />
            public override Try<TResult> AndThenTry<TResult>(Func<T, Result<TResult>> bind) 
                => FN.Try(() => AndThen(bind));

            /// <inheritdoc />
            public override Try<TResult> AndThenTry<TResult>(Func<T, Try<TResult>> bind) 
                => Functional.Try.Lift(this).AndThen(bind);
        }
    }
}
