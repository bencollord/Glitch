namespace Glitch.Functional
{
    public static partial class Result
    {
        public static Result<T> Ok<T>(T value) => new Okay<T>(value);

        public static Result<T> Fail<T>(Error error) => new Failure<T>(error);

        public static Result<TResult> Apply<T, TResult>(this Result<Func<T, TResult>> function, Result<T> value)
            => value.Apply(function);
    }

    public abstract class Result<T> : IEquatable<Result<T>>
    {
        public abstract bool IsOkay { get; }

        public abstract bool IsFail { get; }

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

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T> Do(Action<T> action);

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
        /// Returns the wrapped value if Ok. Otherwise throws the wrapped error
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
        public abstract Option<T> ToOption();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<T> Iterate();

        public abstract bool Equals(Result<T>? other);

        public override bool Equals(object? obj) => Equals(obj as Result<T>);

        public abstract override int GetHashCode();

        public abstract override string ToString();

        public static bool operator true(Result<T> result) => result.IsOkay;

        public static bool operator false(Result<T> result) => result.IsFail;

        public static Result<T> operator &(Result<T> x, Result<T> y) => x.And(y);

        public static Result<T> operator |(Result<T> x, Result<T> y) => x.Or(y);

        public static implicit operator bool(Result<T> result) => result.IsOkay;

        public static implicit operator Result<T>(T value) => new Result.Okay<T>(value);

        public static implicit operator Result<T>(Error error) => new Result.Failure<T>(error);

        public static bool operator ==(Result<T>? x, Result<T>? y)
            => x is null ? y is null : x.Equals(y);

        public static bool operator !=(Result<T>? x, Result<T>? y) => !(x == y);
    }
}
