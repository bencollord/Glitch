namespace Glitch.Functional
{
    public abstract record Result<T>
    {
        public static Result<T> Okay(T value) => new Result.Okay<T>(value);

        public static Result<T> Fail(Error error) => new Result.Fail<T>(error);

        public abstract bool IsOkay { get; }

        public abstract bool IsFail { get; }

        public abstract bool IsOkayAnd(Func<T, bool> predicate);

        public abstract bool IsFailAnd(Func<Error, bool> predicate);

        /// <summary>
        /// If the result is <see cref="Result.Okay{T}" />, applies
        /// the provided function to the value and returns it wrapped in a
        /// new <see cref="Result{T}" />. Otherwise, returns the current error
        /// wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public abstract Result<TResult> Map<TResult>(Func<T, TResult> mapper);

        public abstract Result<TResult> MapOr<TResult>(Func<T, TResult> mapper, TResult ifFail);

        public abstract Result<TResult> MapOrElse<TResult>(Func<T, TResult> mapper, Func<Error, TResult> ifFail);

        public abstract Result<TResult> MapOr<TResult>(Func<T, TResult> mapper, Error ifFail);

        public abstract Result<TResult> MapOrElse<TResult>(Func<T, TResult> mapper, Func<Error, Error> ifFail);

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public abstract Result<T> MapError(Func<Error, Error> mapper);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns a faulted <see cref="Result{TResult}" /> containing the 
        /// error value of self if it exists or the error value of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Result<TResult> Apply<TResult>(Result<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<TResult> And<TResult>(Result<TResult> other);

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public abstract Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> mapper);

        public Result<TResult> AndThen<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Returns the current result if Ok, otherwise returns the other result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<T> Or(Result<T> other);

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<T> OrElse(Func<Error, Result<T>> other);

        public abstract Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> ifOkay, Func<Error, Result<TResult>> ifFail);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> Do(Action<T> action) => IfOkay(action); // TODO Not sure how I feel about aliasing methods like this.

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T> IfOkay(Action<T> action);

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T> IfFail(Action action);

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T> IfFail(Action<Error> action);

        /// <summary>
        /// Throws the error as an exception if fail. If okay, does nothing.
        /// </summary>
        public abstract void ThrowIfFail();

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function to the wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifError"></param>
        /// <returns></returns>
        public abstract TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifError);

        /// <summary>
        /// Casts the wrapped value to <typeparamref name="TResult"/> if Ok,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public abstract Result<TResult> Cast<TResult>();

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns an <see cref="ApplicationError"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public abstract Result<T> Filter(Func<T, bool> predicate);

        /// <summary>
        /// A map operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Fallible<TResult> Try<TResult>(Func<T, TResult> map);

        /// <summary>
        /// A bind operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Fallible<TResult> AndThenTry<TResult>(Func<T, Result<TResult>> bind);

        /// <summary>
        /// A bind operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Fallible<TResult> AndThenTry<TResult>(Func<T, Fallible<TResult>> bind);

        /// <summary>
        /// Combines another result into a result of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Result<(T, TOther)> Zip<TOther>(Result<TOther> other)
            => ZipWith(other, (x, y) => (x, y));

        /// <summary>
        /// Combines two results using a provided function if both are okay.
        /// Otherwise, returns the error value of whichever one failed.
        /// If both are faulted, returns an <see cref="AggregateError>"/>.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public abstract Result<TResult> ZipWith<TOther, TResult>(Result<TOther> other, Func<T, TOther, TResult> zipper);

        /// <summary>
        /// Returns the wrapped value if ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <returns></returns>
        public abstract T Unwrap();

        /// <summary>
        /// Returns the wrapped value if Ok, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract T UnwrapOr(T fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract T UnwrapOrElse(Func<T> fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function applied to the wrapped error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract T UnwrapOrElse(Func<Error, T> fallback);

        /// <summary>
        /// Returns Some(<typeparamref name="T" />) if Ok. Otherwise, returns
        /// an empty <see cref="Option{T}" />.
        /// </summary>
        /// <returns></returns>
        public abstract Option<T> UnwrapOrNone();

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns></returns>
        public Error UnwrapError()
            => UnwrapErrorOrElse(() => new InvalidOperationException("Cannot unwrap error of successful result"));

        /// <summary>
        /// Returns the wrapped error if faulted otherwise returns the fallback error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract Error UnwrapErrorOr(Error fallback);

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public Error UnwrapErrorOrElse(Func<Error> fallback)
            => UnwrapErrorOrElse(_ => fallback());

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract Error UnwrapErrorOrElse(Func<T, Error> fallback);

        /// <summary>
        /// Returns Some(<see cref="Error"/>) if faulted. Otherwise, returns
        /// an empty <see cref="Option{Error}"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Option<Error> UnwrapErrorOrNone();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<T> Iterate();

        public OneOf<T, Error> AsLeft() => LeftOrElse(Identity);

        public OneOf<Error, T> AsRight() => RightOrElse(Identity);

        public OneOf<T, TRight> LeftOr<TRight>(TRight value) => LeftOrElse(_ => value);

        public OneOf<T, TRight> LeftOrElse<TRight>(Func<Error, TRight> func)
            => Match(OneOf<T, TRight>.Left, func.Then(OneOf<T, TRight>.Right));

        public OneOf<TLeft, T> RightOr<TLeft>(TLeft value) => RightOrElse(_ => value);

        public OneOf<TLeft, T> RightOrElse<TLeft>(Func<Error, TLeft> func)
            => Match(OneOf<TLeft, T>.Right, func.Then(OneOf<TLeft, T>.Left));

        public abstract override string ToString();

        public static bool operator true(Result<T> result) => result.IsOkay;

        public static bool operator false(Result<T> result) => result.IsFail;

        public static Result<T> operator &(Result<T> x, Result<T> y) => x.And(y);

        public static Result<T> operator |(Result<T> x, Result<T> y) => x.Or(y);

        public static implicit operator bool(Result<T> result) => result.IsOkay;

        public static implicit operator Result<T>(T value) => Okay(value);

        public static implicit operator Result<T>(Error error) => Fail(error);

        public static explicit operator T(Result<T> result) 
            => result.MapError(err => new InvalidCastException($"Cannot cast a faulted result to a value", err.AsException()))
                     .Unwrap();

        public static explicit operator Error(Result<T> result)
            => result is Result.Fail<T>(var err) 
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");
    };
}
