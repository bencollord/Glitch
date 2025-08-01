using System.Collections.Immutable;

namespace Glitch.Functional
{
    public partial record Result<T>
    {
        private Result<T, Error> inner;

        private protected Result(Result<T, Error> inner) 
        {
            this.inner = inner;
        }

        public static Result<T> Okay(T value) => new Result.Success<T>(value);

        public static Result<T> Fail(Error error) => new Result.Failure<T>(error);

        public bool IsOkay => inner.IsOkay;

        public bool IsFail => inner.IsFail;

        public bool IsOkayAnd(Func<T, bool> predicate) => inner.IsOkayAnd(predicate);

        public bool IsFailOr(Func<T, bool> predicate) => inner.IsErrorOr(predicate);

        /// <summary>
        /// If the result is <see cref="Result.Success{T}" />, applies
        /// the provided function to the value and returns it wrapped in a
        /// new <see cref="Result{T}" />. Otherwise, returns the current error
        /// wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Result<TResult> Map<TResult>(Func<T, TResult> map) => new(inner.Map(map));

        public Result<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public Result<T> MapError(Func<Error, Error> map) => inner.MapError(map);

        /// <summary>
        /// If the result is a failure, returns a new <see cref="Result{TOkay, TError}"/>
        /// with the mapping function applied to the error. Otherwise, returns the okay
        /// value of self wrapped in the <see cref="Result{TOkay, TError}"/> type.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Result<T, E> MapError<E>(Func<Error, E> map) => inner.MapError(map);

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
        public Result<TResult> And<TResult>(Result<TResult> other) => IsOkay ? other : Cast<TResult>();

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> bind)
            => inner.AndThen(x => bind(x).inner);

        /// <summary>
        /// BindMap operation, similar to the two arg overload of SelectMany.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Result<TResult> AndThen<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Returns the current result if Ok, otherwise returns the other result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Result<T> Or(Result<T> other) => IsOkay ? this : other;

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Result<T> OrElse(Func<Error, Result<T>> bind)
            => inner.OrElse(x => bind(x).inner);

        /// <summary>
        /// BiBind operation
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Result<TResult> Choose<TResult>(Func<T, Result<TResult>> okay, Func<Error, Result<TResult>> error)
            => inner.Choose(v => okay(v).inner, e => error(e).inner);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> Do(Action<T> action) => inner.Do(action);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> Do(Func<T, Unit> action) => inner.Do(action);

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> IfFail(Action action) => inner.IfFail(action);

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> IfFail(Action<Error> action) => inner.IfFail(action);

        public TResult Match<TResult>(Func<T, TResult> okay, TResult error)
            => Map(okay).IfFail(error);

        public TResult Match<TResult>(Func<T, TResult> okay, Func<TResult> error)
            => Match(okay, _ => error());

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function to the wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> fail)
            => inner.Match(okay, fail);

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
        public Result<TResult> Cast<TResult>() => inner.Cast<TResult>();

        /// <summary>
        /// Casts the result, or returns the provided error
        /// if the cast fails.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public Result<TResult> CastOr<TResult>(Error error) => inner.CastOr<TResult>(error);

        public Result<TResult> CastOrElse<TResult>(Func<T, Error> error) => inner.CastOrElse<TResult>(error);

        public Result<T> Filter(Func<T, bool> predicate)
            => Guard(predicate, Error.Empty);

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns a the provided <paramref name="error"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Result<T> Guard(Func<T, bool> predicate, Error error)
            => inner.Guard(predicate, error);

        public Result<T> Guard(Func<T, bool> predicate, Func<T, Error> error)
            => inner.Guard(predicate, error);

        public Result<T> GuardNot(Func<T, bool> predicate, Error error)
            => inner.GuardNot(predicate, error);

        public Result<T> GuardNot(Func<T, bool> predicate, Func<T, Error> error)
            => inner.GuardNot(predicate, error);

        public Result<T> Guard(bool condition, Error error)
            => inner.Guard(condition, error);

        public Result<T> Guard(bool condition, Func<T, Error> error)
            => inner.Guard(condition, error);

        public Result<T> GuardNot(bool condition, Error error)
            => inner.GuardNot(condition, error);

        public Result<T> GuardNot(bool condition, Func<T, Error> error)
            => inner.GuardNot(condition, error);

        public Result<(T, TOther)> Zip<TOther>(Result<TOther> other)
            => Zip(other, (x, y) => (x, y));

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
        public Result<TResult> Zip<TOther, TResult>(Result<TOther> other, Func<T, TOther, TResult> zipper)
            => AndThen(_ => other, zipper);

        /// <summary>
        /// Returns the wrapped value if ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <returns></returns>
        public T Unwrap() => inner.Unwrap();

        public T UnwrapOr(T fallback) => inner.UnwrapOr(fallback);

        public bool TryUnwrap(out T result) => inner.TryUnwrap(out result);

        public bool TryUnwrapError(out Error result) => inner.TryUnwrapError(out result);

        /// <summary>
        /// Returns the wrapped value if Ok, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfFail(T fallback) => inner.IfFail(fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfFail(Func<T> fallback) => inner.IfFail(fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function applied to the wrapped error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfFail(Func<Error, T> fallback) => inner.IfFail(fallback);

        /// <summary>
        /// Returns Some(<typeparamref name="T" />) if Ok. Otherwise, returns
        /// an empty <see cref="Option{T}" />.
        /// </summary>
        /// <returns></returns>
        public Option<T> OkayOrNone() => inner.OkayOrNone();

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns></returns>
        public Error UnwrapError()
            => inner.UnwrapError();

        /// <summary>
        /// Returns the wrapped error if faulted otherwise returns the fallback error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public Error UnwrapErrorOr(Error fallback)
            => inner.UnwrapErrorOr(fallback);

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public Error UnwrapErrorOrElse(Func<Error> fallback)
            => inner.UnwrapErrorOrElse(fallback);

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public Error UnwrapErrorOrElse(Func<T, Error> fallback)
            => inner.UnwrapErrorOrElse(fallback);

        /// <summary>
        /// Returns Some(<see cref="Error"/>) if faulted. Otherwise, returns
        /// an empty <see cref="Option{Error}"/>.
        /// </summary>
        /// <returns></returns>
        public Option<Error> ErrorOrNone() => inner.ErrorOrNone();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Iterate() => inner.Iterate();

        public Fallible<T> AsFallible() => Fallible.Lift(this);

        public Effect<TInput, T> AsEffect<TInput>() => Effect<TInput, T>.Lift(this);

        public override string ToString() => inner.ToString();

        public static bool operator true(Result<T> result) => result.IsOkay;

        public static bool operator false(Result<T> result) => result.IsFail;

        public static Result<T> operator &(Result<T> x, Result<T> y) => x.And(y);

        public static Result<T> operator |(Result<T> x, Result<T> y) => x.Or(y);

        public static implicit operator bool(Result<T> result) => result.IsOkay;

        public static implicit operator Result<T>(T value) => Okay(value);

        public static implicit operator Result<T>(Success<T> success) => Okay(success.Value);

        public static implicit operator Result<T>(Error error) => Fail(error);

        public static implicit operator Result<T>(Failure<Error> failure) => Fail(failure.Error);

        public static implicit operator Result<T>(Result<T, Error> result) => new(result);

        public static explicit operator Result<T, Error>(Result<T> result) => result.Match(Result<T, Error>.Okay, Result<T, Error>.Fail);

        public static explicit operator T(Result<T> result)
            => result.MapError(err => new InvalidCastException($"Cannot cast a faulted result to a value", err.AsException()))
                     .Unwrap();

        public static explicit operator Error(Result<T> result)
            => result is Result.Failure<T>(var err)
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");

        // UNDONE Needs more comprehensive functionality
        public FluentActionContext IfOkay(Func<T, Unit> ifOkay) => IfOkay(new Action<T>(t => ifOkay(t)));

        public FluentActionContext IfOkay(Action<T> ifOkay) => new FluentActionContext(this, ifOkay);

        // UNDONE Naming inconsistency
        public FluentActionContext ForError<TError>(Action<TError> ifError)
            where TError : Error
            => IfOkay(_ => { /* Nop */ }).Catch(ifError);

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
            private readonly Result<T> result;
            private readonly Action<T> ifOkay;
            private readonly ImmutableDictionary<Type, Action<Error>> errorHandlers;

            internal FluentActionContext(Result<T> result, Action<T> ifOkay) 
                : this(result, ifOkay, ImmutableDictionary<Type, Action<Error>>.Empty) { }

            internal FluentActionContext(Result<T> result, Action<T> ifOkay, ImmutableDictionary<Type, Action<Error>> errorHandlers)
            {
                this.result = result;
                this.ifOkay = ifOkay;
                this.errorHandlers = errorHandlers;
            }

            public FluentActionContext Then(Action<T> ifOkay) => new(result, this.ifOkay + ifOkay, errorHandlers);

            public FluentActionContext Then(Func<T, Unit> ifOkay) => Then(new Action<T>(v => ifOkay(v)));

            public FluentActionContext Catch<TError>(Action<TError> ifError)
                where TError : Error
                => new(result, ifOkay, errorHandlers.Add(typeof(TError), err => ifError((TError)err)));

            public Unit Otherwise(Func<Error, Unit> ifFail) => Otherwise(new Action<Error>(v => ifFail(v)));

            public Unit Otherwise(Action<Error> ifFail)
            {
                switch (result)
                {
                    case Result.Success<T>(T value):
                        ifOkay(value);
                        break;

                    case Result.Failure<T>(Error err)
                        when errorHandlers.TryGetValue(err.GetType(), out var handler):
                        handler(err);
                        break;

                    case Result.Failure<T>(Error err):
                        ifFail(err);
                        break;

                    default:
                        throw BadMatchException();
                }

                return Unit.Value;
            }

            public Unit OtherwiseThrow() => Otherwise(err => err.Throw());

            public Unit OtherwiseDoNothing() => Otherwise(_ => { /* Nop */ });

            public Result<T> OtherwiseContinue() => Otherwise(_ => { /* Nop */ }).Return(result);
        }
    }
}
