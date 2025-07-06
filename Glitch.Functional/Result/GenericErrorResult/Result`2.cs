using System.Collections.Immutable;

namespace Glitch.Functional
{
    public abstract partial record Result<TOkay, TError>
    {
        private protected Result() { }

        public static Result<TOkay, TError> Okay(TOkay value) => new Result.Okay<TOkay, TError>(value);

        public static Result<TOkay, TError> Fail(TError error) => new Result.Fail<TOkay, TError>(error);

        public abstract bool IsOkay { get; }

        public abstract bool IsFail { get; }

        public abstract bool IsOkayAnd(Func<TOkay, bool> predicate);

        public abstract bool IsFailAnd(Func<TError, bool> predicate);

        /// <summary>
        /// If the result is <see cref="Result.Okay{T}" />, applies
        /// the provided function to the value and returns it wrapped in a
        /// new <see cref="Result{T}" />. Otherwise, returns the current error
        /// wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Result<TResult, TError> Map<TResult>(Func<TOkay, TResult> map);

        public Result<Func<T2, TResult>, TError> PartialMap<T2, TResult>(Func<TOkay, T2, TResult> map)
            => Map(map.Curry());

        public abstract Result<TResult, TError> MapOr<TResult>(Func<TOkay, TResult> map, TError ifFail);

        public abstract Result<TResult, TError> MapOrElse<TResult>(Func<TOkay, TResult> map, Func<TError, TError> ifFail);

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Result<TOkay, TNewError> MapError<TNewError>(Func<TError, TNewError> map);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns a faulted <see cref="Result{TResult}" /> containing the 
        /// error value of self if it exists or the error value of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Result<TResult, TError> Apply<TResult>(Result<Func<TOkay, TResult>, TError> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<TResult, TError> And<TResult>(Result<TResult, TError> other);

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public abstract Result<TResult, TError> AndThen<TResult>(Func<TOkay, Result<TResult, TError>> bind);

        /// <summary>
        /// BindMap operation, similar to the two arg overload of SelectMany.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Result<TResult, TError> AndThen<TElement, TResult>(Func<TOkay, Result<TElement, TError>> bind, Func<TOkay, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Returns the current result if Ok, otherwise returns the other result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<TOkay, TError> Or(Result<TOkay, TError> other);

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<TOkay, TError> OrElse(Func<TError, Result<TOkay, TError>> other);

        /// <summary>
        /// BiBind operation
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifFail"></param>
        /// <returns></returns>
        public abstract Result<TResult, TError> Choose<TResult>(Func<TOkay, Result<TResult, TError>> ifOkay, Func<TError, Result<TResult, TError>> ifFail);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<TOkay, TError> Do(Action<TOkay> action);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<TOkay, TError> Do(Func<TOkay, Unit> action) => Do(v => action(v));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<TOkay, TError> IfFail(Action action) => IfFail(_ => action());

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<TOkay, TError> IfFail(Action<TError> action);

        /// <summary>
        /// Executes an impure action if failed and the error matches the provided type.
        /// No op if Okay or a different error type.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<TOkay, TError> IfError<TDerivedError>(Action action)
            where TDerivedError : TError
            => IfError<TError>(_ => action());

        /// <summary>
        /// Executes an impure action if failed and the error matches the provided type.
        /// No op if Okay or a different error type.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<TOkay, TError> IfError<TDerivedError>(Action<TDerivedError> action)
            where TDerivedError : TError;

        public TResult Match<TResult>(Func<TOkay, TResult> ifOkay, TResult ifFail)
            => Map(ifOkay).IfFail(ifFail);

        public TResult Match<TResult>(Func<TOkay, TResult> ifOkay, Func<TResult> ifFail)
            => Match(ifOkay, _ => ifFail());

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function to the wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifFail"></param>
        /// <returns></returns>
        public abstract TResult Match<TResult>(Func<TOkay, TResult> ifOkay, Func<TError, TResult> ifFail);

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
        public abstract Result<TResult, TError> Cast<TResult>();

        /// <summary>
        /// Casts the result, or returns the provided error
        /// if the cast fails.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public abstract Result<TResult, TError> CastOr<TResult>(TError error);

        public abstract Result<TResult, TError> CastOrElse<TResult>(Func<TOkay, TError> error);

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns a the provided <paramref name="error"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public abstract Result<TOkay, TError> Guard(Func<TOkay, bool> predicate, TError error);

        public abstract Result<TOkay, TError> Guard(Func<TOkay, bool> predicate, Func<TOkay, TError> error);

        public Result<TOkay, TError> GuardNot(Func<TOkay, bool> predicate, TError error)
            => Guard(predicate.Not(), error);

        public Result<TOkay, TError> GuardNot(Func<TOkay, bool> predicate, Func<TOkay, TError> error)
            => Guard(predicate.Not(), error);

        public Result<TOkay, TError> Guard(bool condition, TError error)
            => Guard(_ => condition, error);

        public Result<TOkay, TError> Guard(bool condition, Func<TOkay, TError> error)
            => Guard(_ => condition, error);

        public Result<TOkay, TError> GuardNot(bool condition, TError error)
            => Guard(!condition, error);

        public Result<TOkay, TError> GuardNot(bool condition, Func<TOkay, TError> error)
            => Guard(!condition, error);

        /// <summary>
        /// Returns the wrapped value if ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <returns></returns>
        public abstract TOkay Unwrap();

        public TOkay UnwrapOr(TOkay fallback) => IfFail(fallback);

        public abstract bool TryUnwrap(out TOkay result);

        public abstract bool TryUnwrapError(out TError result);

        /// <summary>
        /// Returns the wrapped value if Ok, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract TOkay IfFail(TOkay fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract TOkay IfFail(Func<TOkay> fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function applied to the wrapped error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract TOkay IfFail(Func<TError, TOkay> fallback);

        /// <summary>
        /// Returns Some(<typeparamref name="T" />) if Ok. Otherwise, returns
        /// an empty <see cref="Option{T}" />.
        /// </summary>
        /// <returns></returns>
        public abstract Option<TOkay> OrNone();

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns></returns>
        public TError UnwrapError()
            => UnwrapErrorOrElse(() => throw new InvalidOperationException("Cannot unwrap error of successful result"));

        /// <summary>
        /// Returns the wrapped error if faulted otherwise returns the fallback error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract TError UnwrapErrorOr(TError fallback);

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public TError UnwrapErrorOrElse(Func<TError> fallback)
            => UnwrapErrorOrElse(_ => fallback());

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public abstract TError UnwrapErrorOrElse(Func<TOkay, TError> fallback);

        /// <summary>
        /// Returns Some(<see cref="Error"/>) if faulted. Otherwise, returns
        /// an empty <see cref="Option{Error}"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Option<TError> ErrorOrNone();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TOkay> Iterate();

        public abstract override string ToString();

        public static bool operator true(Result<TOkay, TError> result) => result.IsOkay;

        public static bool operator false(Result<TOkay, TError> result) => result.IsFail;

        public static Result<TOkay, TError> operator &(Result<TOkay, TError> x, Result<TOkay, TError> y) => x.And(y);

        public static Result<TOkay, TError> operator |(Result<TOkay, TError> x, Result<TOkay, TError> y) => x.Or(y);

        public static implicit operator bool(Result<TOkay, TError> result) => result.IsOkay;

        public static implicit operator Result<TOkay, TError>(TOkay value) => Okay(value);

        public static implicit operator Result<TOkay, TError>(TError error) => Fail(error);

        public static explicit operator TOkay(Result<TOkay, TError> result)
            => Try(result.Unwrap)
                   .MapError(err => new InvalidCastException($"Cannot cast a faulted result to {typeof(TOkay)}", err.AsException()))
                   .Unwrap();

        public static explicit operator TError(Result<TOkay, TError> result)
            => result is Result.Fail<TOkay, TError>(var err)
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");

        // UNDONE Needs more comprehensive functionality
        public FluentActionContext IfOkay(Func<TOkay, Unit> ifOkay) => IfOkay(new Action<TOkay>(t => ifOkay(t)));

        public FluentActionContext IfOkay(Action<TOkay> ifOkay) => new FluentActionContext(this, ifOkay);

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
            private readonly Result<TOkay, TError> result;
            private readonly Action<TOkay> ifOkay;
            private readonly ImmutableDictionary<Type, Action<TError>> errorHandlers;

            internal FluentActionContext(Result<TOkay, TError> result, Action<TOkay> ifOkay)
                : this(result, ifOkay, ImmutableDictionary<Type, Action<TError>>.Empty) { }

            internal FluentActionContext(Result<TOkay, TError> result, Action<TOkay> ifOkay, ImmutableDictionary<Type, Action<TError>> errorHandlers)
            {
                this.result = result;
                this.ifOkay = ifOkay;
                this.errorHandlers = errorHandlers;
            }

            public FluentActionContext Then(Action<TOkay> ifOkay) => new(result, this.ifOkay + ifOkay, errorHandlers);

            public FluentActionContext Then(Func<TOkay, Unit> ifOkay) => Then(new Action<TOkay>(v => ifOkay(v)));

            public Unit Otherwise(Func<TError, Unit> ifFail) => Otherwise(new Action<TError>(v => ifFail(v)));

            public Unit Otherwise(Action<TError> ifFail)
            {
                switch (result)
                {
                    case Result.Okay<TOkay, TError>(TOkay value):
                        ifOkay(value);
                        break;

                    case Result.Fail<TOkay, TError>(TError err)
                        when errorHandlers.TryGetValue(err.GetType(), out var handler):
                        handler(err);
                        break;

                    case Result.Fail<TOkay, TError>(TError err):
                        ifFail(err);
                        break;

                    default:
                        throw BadMatchException();
                }

                return Unit.Value;
            }

            public Unit OtherwiseDoNothing() => Otherwise(_ => { /* Nop */ });

            public Result<TOkay, TError> OtherwiseContinue() => Otherwise(_ => { /* Nop */ }).Return(result);
        }
    }
}