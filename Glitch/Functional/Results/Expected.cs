using Glitch.Functional.Attributes;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    [Monad]
    public abstract partial record Expected<T, E>
    {
        private protected Expected() { }

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T, E> Okay(T value) => new Expected.Success<T, E>(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T, E> Fail(E error) => new Expected.Failure<T, E>(error);

        public abstract bool IsOkay { get; }

        public abstract bool IsError { get; }

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsOkayAnd(Func<T, bool> predicate) => Match(predicate, false);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsErrorOr(Func<T, bool> predicate) => Match(predicate, true);

        /// <summary>
        /// If the result is <see cref="Expected.Success{T}" />, applies
        /// the provided function to the value and returns it wrapped in a
        /// new <see cref="Result{T}" />. Otherwise, returns the current error
        /// wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Expected<TResult, E> Select<TResult>(Func<T, TResult> map);

        public Expected<Func<T2, TResult>, E> PartialSelect<T2, TResult>(Func<T, T2, TResult> map)
            => Select(map.Curry());

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<T, TNewError> SelectError<TNewError>(Func<E, TNewError> map);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns a faulted <see cref="Result{TResult}" /> containing the 
        /// error value of self if it exists or the error value of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult, E> Apply<TResult>(Expected<Func<T, TResult>, E> function)
            => AndThen(v => function.Select(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<TResult, E> And<TResult>(Expected<TResult, E> other);

        public Expected<TResult, E> And<TResult>(Success<TResult> other)
            => And(Expected.Okay<TResult, E>(other.Value));

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<TResult, E> AndThen<TResult>(Func<T, Expected<TResult, E>> bind);

        /// <summary>
        /// BindMap operation, similar to the two arg overload of SelectMany.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult, E> AndThen<TElement, TResult>(Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));

        /// <summary>
        /// Returns the current result if Ok, otherwise returns the other result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<T, EResult> Or<EResult>(Expected<T, EResult> other);

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<T, EResult> OrElse<EResult>(Func<E, Expected<T, EResult>> other);

        /// <summary>
        /// BiBind operation
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult, EResult> Choose<TResult, EResult>(Func<T, Expected<TResult, EResult>> okay, Func<E, Expected<TResult, EResult>> error) => Match(okay, error);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T, E> Do(Action<T> action) => Do(action.Return());

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T, E> Do(Func<T, Unit> action) => Select(x => action(x).Return(x));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T, E> IfFail(Action action) => IfFail(_ => action());

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<T, E> IfFail(Action<E> action);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> okay, TResult error)
            => Match(okay, _ => error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> error);

        public Unit Match(Action<T> okay, Action<E> error) => Match(okay.Return(), error.Return());

        /// <summary>
        /// If Okay, casts the wrapped value to <typeparamref name="TResult"/>,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <exception cref="InvalidCastException">
        /// If the cast is not valid. If you need safe casting,
        /// lift the result into the <see cref="Effect{T}"/> type,
        /// which will not throw.
        /// </exception>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns a the provided <paramref name="error"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<T, E> Guard(Func<T, bool> predicate, E error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Expected<T, E> Guard(Func<T, bool> predicate, Func<T, E> error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T, E> Guard(bool condition, E error)
            => Guard(_ => condition, error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T, E> Guard(bool condition, Func<T, E> error)
            => Guard(_ => condition, error);

        /// <summary>
        /// Combines another result into a result of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<(T, TOther), E> Zip<TOther>(Expected<TOther, E> other)
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
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult, E> Zip<TOther, TResult>(Expected<TOther, E> other, Func<T, TOther, TResult> zipper)
            => AndThen(_ => other, zipper);

        /// <summary>
        /// Returns the wrapped value if ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract T Unwrap();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T UnwrapOr(T fallback) => IfFail(fallback);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool TryUnwrap(out T result);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool TryUnwrapError(out E result);

        /// <summary>
        /// Returns the wrapped value if Ok, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract T IfFail(T fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T IfFail(Func<T> fallback) => IfFail(_ => fallback());

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function applied to the wrapped error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract T IfFail(Func<E, T> fallback);

        /// <summary>
        /// Returns Some(<typeparamref name="T" />) if Ok. Otherwise, returns
        /// an empty <see cref="Option{T}" />.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Option<T> OkayOrNone();

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E UnwrapError()
            => UnwrapErrorOrElse(() => throw new InvalidOperationException("Cannot unwrap error of successful result"));

        /// <summary>
        /// Returns the wrapped error if faulted otherwise returns the fallback error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract E UnwrapErrorOr(E fallback);

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public E UnwrapErrorOrElse(Func<E> fallback)
            => UnwrapErrorOrElse(_ => fallback());

        /// <summary>
        /// Returns the wrapped error if faulted. Otherwise, returns the error
        /// produced by the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract E UnwrapErrorOrElse(Func<T, E> fallback);

        /// <summary>
        /// Returns Some(<see cref="Error"/>) if faulted. Otherwise, returns
        /// an empty <see cref="Option{Error}"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Option<E> ErrorOrNone();

        /// <summary>
        /// Returns a singleton <see cref="IEnumerable{T}" /> if Ok.
        /// Otherwise, yields and empty <see cref="IEnumerable{T}" .
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IEnumerable<T> Iterate();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract override string ToString();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Expected<T, E> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator false(Expected<T, E> result) => result.IsError;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T, E> operator &(Expected<T, E> x, Expected<T, E> y) => x.And(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T, E> operator |(Expected<T, E> x, Expected<T, E> y) => x.Or(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Expected<T, E> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T, E>(T value) => Okay(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T, E>(Success<T> success) => Okay(success.Value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T, E>(E error) => Fail(error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T, E>(Failure<E> failure) => Fail(failure.Error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator T(Expected<T, E> result)
            => result.IfFail(err => throw new InvalidCastException($"Cannot cast a faulted result to {typeof(T)}"));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator E(Expected<T, E> result)
            => result is Expected.Failure<T, E>(var err)
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");
    }
}