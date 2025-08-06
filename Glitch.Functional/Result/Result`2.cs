namespace Glitch.Functional
{
    public abstract partial record Result<T, E>
    {
        private protected Result() { }

        public static Result<T, E> Okay(T value) => new Result.Success<T, E>(value);

        public static Result<T, E> Fail(E error) => new Result.Failure<T, E>(error);

        public abstract bool IsOkay { get; }

        public abstract bool IsError { get; }

        public bool IsOkayAnd(Func<T, bool> predicate) => Match(predicate, false);

        public bool IsErrorOr(Func<T, bool> predicate) => Match(predicate, true);

        /// <summary>
        /// If the result is <see cref="Result.Success{T}" />, applies
        /// the provided function to the value and returns it wrapped in a
        /// new <see cref="Result{T}" />. Otherwise, returns the current error
        /// wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Result<TResult, E> Map<TResult>(Func<T, TResult> map);

        public Result<Func<T2, TResult>, E> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Result<T, TNewError> MapError<TNewError>(Func<E, TNewError> map);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns a faulted <see cref="Result{TResult}" /> containing the 
        /// error value of self if it exists or the error value of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Result<TResult, E> Apply<TResult>(Result<Func<T, TResult>, E> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<TResult, E> And<TResult>(Result<TResult, E> other);

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public abstract Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> bind);

        /// <summary>
        /// BindMap operation, similar to the two arg overload of SelectMany.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Result<TResult, E> AndThen<TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Returns the current result if Ok, otherwise returns the other result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<T, EResult> Or<EResult>(Result<T, EResult> other);

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> other);

        /// <summary>
        /// BiBind operation
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Result<TResult, E> Choose<TResult>(Func<T, Result<TResult, E>> okay, Func<E, Result<TResult, E>> error) => Match(okay, error);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T, E> Do(Action<T> action) => Do(action.Return());

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T, E> Do(Func<T, Nothing> action) => Map(x => action(x).Return(x));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T, E> IfFail(Action action) => IfFail(_ => action());

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T, E> IfFail(Action<E> action);

        public TResult Match<TResult>(Func<T, TResult> okay, TResult error)
            => Match(okay, _ => error);

        public TResult Match<TResult>(Func<T, TResult> okay, Func<TResult> error)
            => Match(okay, _ => error());

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function to the wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public abstract TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> error);

        /// <summary>
        /// If Okay, casts the wrapped value to <typeparamref name="TResult"/>,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <exception cref="InvalidCastException">
        /// If the cast is not valid. If you need safe casting,
        /// lift the result into the <see cref="Effect{T}"/> type.
        /// 
        /// This method is guaranteed not to throw on a failed result.
        /// </exception>
        /// <returns></returns>
        public Result<TResult, E> Cast<TResult>() => Map(DynamicCast<TResult>.From);

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns a the provided <paramref name="error"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public abstract Result<T, E> Guard(Func<T, bool> predicate, E error);

        public abstract Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> error);

        public Result<T, E> GuardNot(Func<T, bool> predicate, E error)
            => Guard(predicate.Not(), error);

        public Result<T, E> GuardNot(Func<T, bool> predicate, Func<T, E> error)
            => Guard(predicate.Not(), error);

        public Result<T, E> Guard(bool condition, E error)
            => Guard(_ => condition, error);

        public Result<T, E> Guard(bool condition, Func<T, E> error)
            => Guard(_ => condition, error);

        public Result<T, E> GuardNot(bool condition, E error)
            => Guard(!condition, error);

        public Result<T, E> GuardNot(bool condition, Func<T, E> error)
            => Guard(!condition, error);

        /// <summary>
        /// Returns the wrapped value if ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <returns></returns>
        public abstract T Unwrap();

        public T UnwrapOr(T fallback) => IfFail(fallback);

        public abstract bool TryUnwrap(out T result);

        public abstract bool TryUnwrapError(out E result);

        /// <summary>
        /// Returns the wrapped value if Ok, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract T IfFail(T fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfFail(Func<T> fallback) => IfFail(_ => fallback());

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function applied to the wrapped error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract T IfFail(Func<E, T> fallback);

        /// <summary>
        /// Returns Some(<typeparamref name="T" />) if Ok. Otherwise, returns
        /// an empty <see cref="Option{T}" />.
        /// </summary>
        /// <returns></returns>
        public abstract Option<T> OkayOrNone();

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns></returns>
        public E UnwrapError()
            => UnwrapErrorOrElse(() => throw new InvalidOperationException("Cannot unwrap error of successful result"));

        /// <summary>
        /// Returns the wrapped error if faulted otherwise returns the fallback error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract E UnwrapErrorOr(E fallback);

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public E UnwrapErrorOrElse(Func<E> fallback)
            => UnwrapErrorOrElse(_ => fallback());

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract E UnwrapErrorOrElse(Func<T, E> fallback);

        /// <summary>
        /// Returns Some(<see cref="Error"/>) if faulted. Otherwise, returns
        /// an empty <see cref="Option{Error}"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Option<E> ErrorOrNone();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<T> Iterate();

        public abstract override string ToString();

        public static bool operator true(Result<T, E> result) => result.IsOkay;

        public static bool operator false(Result<T, E> result) => result.IsError;

        public static Result<T, E> operator &(Result<T, E> x, Result<T, E> y) => x.And(y);

        public static Result<T, E> operator |(Result<T, E> x, Result<T, E> y) => x.Or(y);

        public static implicit operator bool(Result<T, E> result) => result.IsOkay;

        public static implicit operator Result<T, E>(T value) => Okay(value);

        public static implicit operator Result<T, E>(Success<T> success) => Okay(success.Value);

        public static implicit operator Result<T, E>(E error) => Fail(error);

        public static implicit operator Result<T, E>(Failure<E> failure) => Fail(failure.Error);

        public static explicit operator T(Result<T, E> result)
            => Try(result.Unwrap)
                   .Run()
                   .IfFail(err => throw new InvalidCastException($"Cannot cast a faulted result to {typeof(T)}", err.AsException()));

        public static explicit operator E(Result<T, E> result)
            => result is Result.Failure<T, E>(var err)
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");
    }
}