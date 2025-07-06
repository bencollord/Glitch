namespace Glitch.Functional
{
    public abstract partial record Validation<T>
    {
        private protected Validation() { }

        public static Validation<T> Success(T value) => new Validation.Success<T>(value);

        public static Validation<T> Failure(Error error) => new Validation.Failure<T>(error);

        public abstract bool IsSuccess { get; }

        public abstract bool IsFailure { get; }

        public abstract bool IsOkayAnd(Func<T, bool> predicate);

        public abstract bool IsFailAnd(Func<Error, bool> predicate);

        public abstract Validation<TResult> Map<TResult>(Func<T, TResult> map);

        public Validation<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        public abstract Validation<TResult> MapOr<TResult>(Func<T, TResult> map, Error ifFail);

        public abstract Validation<TResult> MapOrElse<TResult>(Func<T, TResult> map, Func<Error, Error> ifFail);

        public abstract Validation<T> MapError(Func<Error, Error> map);

        public Validation<TResult> Apply<TResult>(Validation<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        public abstract Validation<TResult> And<TResult>(Validation<TResult> other);

        public abstract Validation<TResult> AndThen<TResult>(Func<T, Validation<TResult>> bind);

        public Validation<TResult> AndThen<TElement, TResult>(Func<T, Validation<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public abstract Validation<T> Or(Validation<T> other);

        public abstract Validation<T> OrElse(Func<Error, Validation<T>> other);

        public abstract Validation<TResult> Choose<TResult>(Func<T, Validation<TResult>> ifOkay, Func<Error, Validation<TResult>> ifFail);

        public Validation<T> Do(Action<T> action) => IfOkay(action); // TODO Not sure how I feel about aliasing methods like this.

        public abstract Validation<T> IfOkay(Action<T> action);

        public abstract Validation<T> IfFail(Action action);

        public abstract Validation<T> IfFail(Action<Error> action);

        public abstract void ThrowIfFail();

        public Validation<TResult> Match<TResult>(Func<T, TResult> ifOkay, TResult ifFail)
            => Map(ifOkay).IfFail(ifFail);

        public Validation<TResult> Match<TResult>(Func<T, TResult> ifOkay, Func<TResult> ifFail)
            => Match(ifOkay, _ => ifFail());

        public abstract TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail);

        public abstract Validation<TResult> Cast<TResult>();

        public Validation<T> Filter(Func<T, bool> predicate)
            => Guard(predicate, new ApplicationError("Result failed check"));

        public abstract Validation<T> Guard(Func<T, bool> predicate, Error error);

        public abstract Validation<T> Guard(Func<T, bool> predicate, Func<T, Error> error);

        public Validation<T> Guard(bool condition, Error error)
            => Guard(_ => condition, error);

        public Validation<T> Guard(bool condition, Func<T, Error> error)
            => Guard(_ => condition, error);

        public Validation<(T, TOther)> Zip<TOther>(Validation<TOther> other)
            => Zip(other, (x, y) => (x, y));

        public abstract Validation<TResult> Zip<TOther, TResult>(Validation<TOther> other, Func<T, TOther, TResult> zipper);

        public abstract T Unwrap();

        public abstract T IfFail(T fallback);

        public abstract T IfFail(Func<T> fallback);

        public abstract T IfFail(Func<Error, T> fallback);

        public abstract Option<T> UnwrapOrNone();

        public Error UnwrapError()
            => UnwrapErrorOrElse(() => new InvalidOperationException("Cannot unwrap error of successful result"));

        public abstract Error UnwrapErrorOr(Error fallback);

        public Error UnwrapErrorOrElse(Func<Error> fallback)
            => UnwrapErrorOrElse(_ => fallback());

        public abstract Error UnwrapErrorOrElse(Func<T, Error> fallback);

        public abstract Option<Error> UnwrapErrorOrNone();

        public abstract IEnumerable<T> Iterate();

        public abstract override string ToString();

        public static bool operator true(Validation<T> result) => result.IsSuccess;

        public static bool operator false(Validation<T> result) => result.IsFailure;

        public static Validation<T> operator &(Validation<T> x, Validation<T> y) => x.And(y);

        public static Validation<T> operator |(Validation<T> x, Validation<T> y) => x.Or(y);

        public static implicit operator bool(Validation<T> result) => result.IsSuccess;

        public static implicit operator Validation<T>(T value) => Success(value);

        public static implicit operator Validation<T>(Error error) => Failure(error);

        public static explicit operator T(Validation<T> result)
            => result.MapError(err => new InvalidCastException($"Cannot cast a faulted result to a value", err.AsException()))
                     .Unwrap();

        public static explicit operator Error(Validation<T> result)
            => result is Validation.Failure<T>(var err)
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");
    };
}
