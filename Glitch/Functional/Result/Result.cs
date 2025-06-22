using System.Collections.Immutable;

namespace Glitch.Functional
{
    public abstract partial record Result<T>
    {
        private protected Result() { }

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
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Result<TResult> Map<TResult>(Func<T, TResult> map);

        public Result<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        public abstract Result<TResult> MapOr<TResult>(Func<T, TResult> map, Error ifFail);

        public abstract Result<TResult> MapOrElse<TResult>(Func<T, TResult> map, Func<Error, Error> ifFail);

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Result<T> MapError(Func<Error, Error> map);

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
        /// <param name="bind"></param>
        /// <returns></returns>
        public abstract Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> bind);

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
        public abstract Result<T> Or(Result<T> other);

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract Result<T> OrElse(Func<Error, Result<T>> other);

        /// <summary>
        /// BiBind operation
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifFail"></param>
        /// <returns></returns>
        public abstract Result<TResult> Choose<TResult>(Func<T, Result<TResult>> ifOkay, Func<Error, Result<TResult>> ifFail);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T> Do(Action<T> action);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> Do(Func<T, Unit> action) => Do(v => action(v));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> IfFail(Action action) => IfFail(_ => action());

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T> IfFail(Action<Error> action);

        /// <summary>
        /// Executes an impure action if failed and the error matches the provided type.
        /// No op if Okay or a different error type.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Result<T> IfError<TError>(Action action)
            where TError : Error
            => IfError<TError>(_ => action());

        /// <summary>
        /// Executes an impure action if failed and the error matches the provided type.
        /// No op if Okay or a different error type.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Result<T> IfError<TError>(Action<TError> action)
            where TError : Error;

        /// <summary>
        /// Throws the error as an exception if fail. If okay, does nothing.
        /// </summary>
        public abstract Result<T> ThrowIfFail();

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
        public abstract TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifFail);

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
        public abstract Result<TResult> Cast<TResult>();

        /// <summary>
        /// Casts the result, or returns the provided error
        /// if the cast fails.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public abstract Result<TResult> CastOr<TResult>(Error error);

        public abstract Result<TResult> CastOrElse<TResult>(Func<T, Error> error);

        public Result<T> Filter(Func<T, bool> predicate)
            => Guard(predicate, Error.Empty);

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns a the provided <paramref name="error"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public abstract Result<T> Guard(Func<T, bool> predicate, Error error);

        public abstract Result<T> Guard(Func<T, bool> predicate, Func<T, Error> error);

        public Result<T> GuardNot(Func<T, bool> predicate, Error error)
            => Guard(predicate.Not(), error);

        public Result<T> GuardNot(Func<T, bool> predicate, Func<T, Error> error)
            => Guard(predicate.Not(), error);

        public Result<T> Guard(bool condition, Error error)
            => Guard(_ => condition, error);

        public Result<T> Guard(bool condition, Func<T, Error> error)
            => Guard(_ => condition, error);

        public Result<T> GuardNot(bool condition, Error error)
            => Guard(!condition, error);

        public Result<T> GuardNot(bool condition, Func<T, Error> error)
            => Guard(!condition, error);

        /// <summary>
        /// A map operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Fallible<TResult> Try<TResult>(Func<T, TResult> map) => Fallible.Lift(this).Map(map);

        public Fallible<T> Try(Action<T> action) => Fallible.Lift(this).Do(action);

        /// <summary>
        /// A bind operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Fallible<TResult> AndThenTry<TResult>(Func<T, Result<TResult>> bind)
            => Fallible.Lift(this).AndThen(bind);

        /// <summary>
        /// A bind operation that wraps the result in
        /// a <see cref="Try{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Fallible<TResult> AndThenTry<TResult>(Func<T, Fallible<TResult>> bind)
            => Fallible.Lift(this).AndThen(bind);

        /// <summary>
        /// Combines another result into a result of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
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
        public abstract Result<TResult> Zip<TOther, TResult>(Result<TOther> other, Func<T, TOther, TResult> zipper);

        /// <summary>
        /// Returns the wrapped value if ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <returns></returns>
        public abstract T Unwrap();

        public T UnwrapOr(T fallback) => IfFail(fallback);

        public abstract bool TryUnwrap(out T result);

        public abstract bool TryUnwrapError(out Error result);

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
        public abstract T IfFail(Func<Error, T> fallback);

        /// <summary>
        /// Returns Some(<typeparamref name="T" />) if Ok. Otherwise, returns
        /// an empty <see cref="Option{T}" />.
        /// </summary>
        /// <returns></returns>
        public abstract Option<T> OrNone();

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
        public abstract Option<Error> ErrorOrNone();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<T> Iterate();

        public Fallible<T> AsFallible() => Fallible.Lift(this);

        public Effect<TInput, T> AsEffect<TInput>() => Effect<TInput, T>.Lift(this);

        public OneOf<T, Error> AsLeft() => LeftOrElse(x => x);

        public OneOf<Error, T> AsRight() => RightOrElse(x => x);

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
                    case Result.Okay<T>(T value):
                        ifOkay(value);
                        break;

                    case Result.Fail<T>(Error err)
                        when errorHandlers.TryGetValue(err.GetType(), out var handler):
                        handler(err);
                        break;

                    case Result.Fail<T>(Error err):
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
