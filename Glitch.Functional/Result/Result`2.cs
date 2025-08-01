using System.Collections.Immutable;

namespace Glitch.Functional
{
    public abstract partial record Result<T, E>
    {
        private protected Result() { }

        public static Result<T, E> Okay(T value) => new Result.Success<T, E>(value);

        public static Result<T, E> Fail(E error) => new Result.Failure<T, E>(error);

        public abstract bool IsOkay { get; }

        public abstract bool IsFail { get; }

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

        public abstract Result<TResult, E> MapOr<TResult>(Func<T, TResult> map, E ifFail);

        public abstract Result<TResult, E> MapOrElse<TResult>(Func<T, TResult> map, Func<E, E> ifFail);

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
        public abstract Result<T, E> Or(Result<T, E> other);

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<T, E> OrElse(Func<E, Result<T, E>> other);

        /// <summary>
        /// BiBind operation
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifFail"></param>
        /// <returns></returns>
        public abstract Result<TResult, E> Choose<TResult>(Func<T, Result<TResult, E>> ifOkay, Func<E, Result<TResult, E>> ifFail);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T, E> Do(Action<T> action);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T, E> Do(Func<T, Unit> action) => Do(v => action(v));

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

        /// <summary>
        /// Executes an impure action if failed and the error matches the provided type.
        /// No op if Okay or a different error type.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T, E> IfError<TDerivedError>(Action action)
            where TDerivedError : E
            => IfError<E>(_ => action());

        /// <summary>
        /// Executes an impure action if failed and the error matches the provided type.
        /// No op if Okay or a different error type.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T, E> IfError<TDerivedError>(Action<TDerivedError> action)
            where TDerivedError : E;

        public TResult Match<TResult>(Func<T, TResult> ifOkay, TResult ifFail)
            => Map(ifOkay).IfFail(ifFail);

        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<TResult> ifFail)
            => Match(ifOkay, _ => ifFail());

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function to the wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifFail"></param>
        /// <returns></returns>
        public abstract TResult Match<TResult>(Func<T, TResult> ifOkay, Func<E, TResult> ifFail);

        /// <summary>
        /// Casts the wrapped value to <typeparamref name="TResult"/> if Ok,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <exception cref="InvalidCastException">
        /// If the cast is not valid. If you need safe casting,
        /// lift the result into the <see cref="Fallible{T}"/> type.
        /// </exception>
        /// <returns></returns>
        public abstract Result<TResult, E> Cast<TResult>();

        /// <summary>
        /// Casts the result, or returns the provided error
        /// if the cast fails.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public abstract Result<TResult, E> CastOr<TResult>(E error);

        public abstract Result<TResult, E> CastOrElse<TResult>(Func<T, E> error);

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
        public abstract T IfFail(Func<T> fallback);

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

        public static bool operator false(Result<T, E> result) => result.IsFail;

        public static Result<T, E> operator &(Result<T, E> x, Result<T, E> y) => x.And(y);

        public static Result<T, E> operator |(Result<T, E> x, Result<T, E> y) => x.Or(y);

        public static implicit operator bool(Result<T, E> result) => result.IsOkay;

        public static implicit operator Result<T, E>(T value) => Okay(value);

        public static implicit operator Result<T, E>(Success<T> success) => Okay(success.Value);

        public static implicit operator Result<T, E>(E error) => Fail(error);

        public static implicit operator Result<T, E>(Failure<E> failure) => Fail(failure.Error);

        public static explicit operator T(Result<T, E> result)
            => Try(result.Unwrap)
                   .MapError(err => new InvalidCastException($"Cannot cast a faulted result to {typeof(T)}", err.AsException()))
                   .Unwrap();

        public static explicit operator E(Result<T, E> result)
            => result is Result.Failure<T, E>(var err)
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");

        // UNDONE Needs more comprehensive functionality
        public FluentActionContext IfOkay(Func<T, Unit> ifOkay) => IfOkay(new Action<T>(t => ifOkay(t)));

        public FluentActionContext IfOkay(Action<T> ifOkay) => new FluentActionContext(this, ifOkay);

        /// <summary>
        /// Fluent context for chaining actions against a result.
        /// 
        /// Experimental API and may be removed.
        /// </summary>
        /// <remarks>
        /// Might remove because this is really stretching the responsibility
        /// of a Result type and kind of turning it more into an effect.
        /// Right now, I'll keep it for convenience, but I'll come back and 
        /// clean this up later.
        /// </remarks>
        public readonly struct FluentActionContext
        {
            private readonly Result<T, E> result;
            private readonly Action<T> ifOkay;
            private readonly ImmutableDictionary<Type, Action<E>> errorHandlers;

            internal FluentActionContext(Result<T, E> result, Action<T> ifOkay)
                : this(result, ifOkay, ImmutableDictionary<Type, Action<E>>.Empty) { }

            internal FluentActionContext(Result<T, E> result, Action<T> ifOkay, ImmutableDictionary<Type, Action<E>> errorHandlers)
            {
                this.result = result;
                this.ifOkay = ifOkay;
                this.errorHandlers = errorHandlers;
            }

            public FluentActionContext Then(Action<T> ifOkay) => new(result, this.ifOkay + ifOkay, errorHandlers);

            public FluentActionContext Then(Func<T, Unit> ifOkay) => Then(new Action<T>(v => ifOkay(v)));

            public Unit Otherwise(Func<E, Unit> ifFail) => Otherwise(new Action<E>(v => ifFail(v)));

            public Unit Otherwise(Action<E> ifFail)
            {
                switch (result)
                {
                    case Result.Success<T, E>(T value):
                        ifOkay(value);
                        break;

                    case Result.Failure<T, E>(E err)
                        when errorHandlers.TryGetValue(err.GetType(), out var handler):
                        handler(err);
                        break;

                    case Result.Failure<T, E>(E err):
                        ifFail(err);
                        break;

                    default:
                        throw BadMatchException();
                }

                return Unit.Value;
            }

            public Unit OtherwiseDoNothing() => Otherwise(_ => { /* Nop */ });

            public Result<T, E> OtherwiseContinue() => Otherwise(_ => { /* Nop */ }).Return(result);
        }
    }
}