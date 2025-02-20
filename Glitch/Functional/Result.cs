using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static partial class Result
    {
        public static Result<TResult> Apply<T, TResult>(this Result<Func<T, TResult>> function, Result<T> value)
            => value.Apply(function);

        public static Result<T> Flatten<T>(this Result<Result<T>> nested)
            => nested.AndThen(n => n);

        public static Option<Result<T>> Invert<T>(this Result<Option<T>> nested)
            => nested.Match(
                    opt => opt.Map(Okay),
                    err => Some(Fail<T>(err))
                );
    }

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
        public abstract Try<TResult> Try<TResult>(Func<T, TResult> map);

        /// <summary>
        /// A bind operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Try<TResult> AndThenTry<TResult>(Func<T, Result<TResult>> bind);

        /// <summary>
        /// A bind operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Try<TResult> AndThenTry<TResult>(Func<T, Try<TResult>> bind);

        /// <summary>
        /// Combines another result into a result of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Result<(T, TOther)> Zip<TOther>(Result<TOther> other)
            => ZipWith(other, (x, y) => (x, y));

        /// <summary>
        /// Combines two results using a provided function.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public Result<TResult> ZipWith<TOther, TResult>(Result<TOther> other, Func<T, TOther, TResult> zipper)
            => AndThen(x => other.Map(y => zipper(x, y)));

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
        public abstract Option<T> UnwrapOrNone();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<T> Iterate();

        public abstract override string ToString();

        public static bool operator true(Result<T> result) => result.IsOkay;

        public static bool operator false(Result<T> result) => result.IsFail;

        public static Result<T> operator &(Result<T> x, Result<T> y) => x.And(y);

        public static Result<T> operator |(Result<T> x, Result<T> y) => x.Or(y);

        public static implicit operator bool(Result<T> result) => result.IsOkay;

        public static implicit operator Result<T>(T value) => Okay(value);

        public static implicit operator Result<T>(Error error) => Fail(error);
    }
}
