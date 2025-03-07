namespace Glitch.Functional
{
    public static partial class Validation
    {
        public record Success<T>(T Value) : Validation<T>
        {
            public override bool IsSuccess => true;

            public override bool IsFailure => false;

            public override Validation<TResult> And<TResult>(Validation<TResult> other) => other;

            public override Validation<TResult> AndThen<TResult>(Func<T, Validation<TResult>> bind) => bind(Value);

            public override Validation<TResult> Cast<TResult>() => DynamicCast<T, TResult>(Value);

            public override Validation<TResult> Choose<TResult>(Func<T, Validation<TResult>> ifOkay, Func<Error, Validation<TResult>> ifFail)
                => AndThen(ifOkay);

            public override Validation<T> Guard(Func<T, bool> predicate, Error error)
                => predicate(Value) ? this : new Failure<T>(error);

            public override Validation<T> Guard(Func<T, bool> predicate, Func<T, Error> error) 
                => predicate(Value) ? this : new Failure<T>(error(Value));

            public override Validation<T> IfFail(Action action) => this;

            public override Validation<T> IfFail(Action<Error> action) => this;

            public override T IfFail(T fallback) => Value;

            public override T IfFail(Func<T> fallback) => Value;

            public override T IfFail(Func<Error, T> fallback) => Value;

            public override Validation<T> IfOkay(Action<T> action)
            {
                action(Value);
                return this;
            }

            public override bool IsFailAnd(Func<Error, bool> predicate) => false;

            public override bool IsOkayAnd(Func<T, bool> predicate) => predicate(Value);

            public override IEnumerable<T> Iterate()
            {
                yield return Value;
            }

            public override Validation<TResult> Map<TResult>(Func<T, TResult> map) => new Success<TResult>(map(Value));

            public override Validation<T> MapError(Func<Error, Error> map) => this;

            public override Validation<TResult> MapOr<TResult>(Func<T, TResult> map, Error ifFail) => Map(map);

            public override Validation<TResult> MapOrElse<TResult>(Func<T, TResult> map, Func<Error, Error> ifFail) => Map(map);

            public override TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail) => ifOkay(Value);

            public override Validation<T> Or(Validation<T> other) => this;

            public override Validation<T> OrElse(Func<Error, Validation<T>> other) => this;

            public override void ThrowIfFail()
            {
                // Nop
            }

            public override T Unwrap()
            {
                return Value;
            }

            public override Error UnwrapErrorOr(Error fallback) => fallback;

            public override Error UnwrapErrorOrElse(Func<T, Error> fallback)
            {
                return fallback(Value);
            }

            public override Option<Error> UnwrapErrorOrNone() => None;

            public override Option<T> UnwrapOrNone() => Value;

            public override Validation<TResult> ZipWith<TOther, TResult>(Validation<TOther> other, Func<T, TOther, TResult> zipper) 
                => AndThen(_ => other, zipper);
        }
    }
}
