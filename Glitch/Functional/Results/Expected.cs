using Glitch.Functional.Attributes;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    [Monad]
    public partial record Expected<T> : IResult<T, Error>
    {
        private Result<T, Error> inner;

        private protected Expected(Result<T, Error> inner) 
        {
            this.inner = inner;
        }

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> Okay(T value) => new Expected.Success<T>(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> Fail(Error error) => new Expected.Failure<T>(error);

        public bool IsOkay => inner.IsOkay;

        public bool IsError => inner.IsError;

        /// <summary>
        /// If the result is <see cref="Result.Success{T}" />, applies
        /// the provided function to the value and returns it wrapped in a
        /// new <see cref="Expected{T}" />. Otherwise, returns the current error
        /// wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult> Select<TResult>(Func<T, TResult> map) => new(inner.Select(map));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<Func<T2, TResult>> PartialSelect<T2, TResult>(Func<T, T2, TResult> map)
            => Select(map.Curry());

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> SelectError(Func<Error, Error> map) => inner.SelectError(e => map(e));

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns a faulted <see cref="Expected{TResult}" /> containing the 
        /// error value of self if it exists or the error value of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult> Apply<TResult>(Expected<Func<T, TResult>> function)
            => AndThen(v => function.Select(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult> And<TResult>(Expected<TResult> other) => IsOkay ? other : Cast<TResult>();

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult> AndThen<TResult>(Func<T, Expected<TResult>> bind)
            => inner.AndThen(x => bind(x).inner);

        /// <summary>
        /// BindMap operation, similar to the two arg overload of SelectMany.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult> AndThen<TElement, TResult>(Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));

        /// <summary>
        /// Returns the current result if Ok, otherwise returns the other result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Or(Expected<T> other) => new(inner.Or(other.inner));

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="bind"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> OrElse(Func<Error, Expected<T>> bind)
            => inner.OrElse(x => bind(x).inner);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> Or<E>(Result<T, E> other) => inner.Or(other);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> OrElse<E>(Func<Error, Result<T, E>> bind)
            => inner.OrElse(x => bind(x));

        /// <summary>
        /// BiBind operation
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult> Choose<TResult>(Func<T, Expected<TResult>> okay, Func<Error, Expected<TResult>> error)
            => inner.Choose(v => okay(v).inner, e => error(e).inner);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Do(Action<T> action) => inner.Do(action);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Do(Func<T, Unit> action) => inner.Do(action);

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> IfFail(Action action) => inner.IfFail(action);

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> IfFail(Action<Error> action) => inner.IfFail(action);

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function to the wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> error)
            => inner.Match(okay, error);

        /// <summary>
        /// Casts the wrapped value to <typeparamref name="TResult"/> if Ok,
        /// otherwise returns the current error wrapped in a new result type.
        /// 
        /// Unlike <see cref="Result{T, E}.Cast{TResult}"/>, will not throw on failure,
        /// but instead returns a new <see cref="Expected{TResult}"/> with the
        /// <see cref="InvalidCastException"/> as its wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<TResult> Cast<TResult>()
            => AndThen(x => DynamicCast<TResult>.Try(x, out var r) ? Expected.Okay(r) : Expected.Fail(Errors.InvalidCast<TResult>(x)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Where(Func<T, bool> predicate)
            => Guard(predicate, Error.Empty);

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns a the provided <paramref name="error"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Guard(Func<T, bool> predicate, Error error)
            => inner.Guard(predicate, error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Guard(Func<T, bool> predicate, Func<T, Error> error)
            => inner.Guard(predicate, error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Guard(bool condition, Error error)
            => inner.Guard(condition, error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> Guard(bool condition, Func<T, Error> error)
            => inner.Guard(condition, error);

        /// <summary>
        /// Combines another result into a result of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<(T, TOther)> Zip<TOther>(Expected<TOther> other)
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
        public Expected<TResult> Zip<TOther, TResult>(Expected<TOther> other, Func<T, TOther, TResult> zipper)
            => AndThen(_ => other, zipper);

        /// <summary>
        /// Returns the wrapped value if ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <remarks>
        /// Overrides the default implementation provided by <see cref="IResult{T, E}"/>
        /// to ensure the exception thrown is the contained <see cref="Error"/> value.
        /// </remarks>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Unwrap() => inner.UnwrapOrElse(err => err.Throw<T>());

        public override string ToString() => inner.ToString();

        // TODO Try and make these methods not necessary
        IResult<TResult, Error> IResult<T, Error>.Select<TResult>(Func<T, TResult> map) => Select(map);

        IResult<T, EResult> IResult<T, Error>.SelectError<EResult>(Func<Error, EResult> map)
            => Match<IResult<T, EResult>>(v => Result.Okay<T, EResult>(v), err => Result.Fail<T, EResult>(map(err)));

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Expected<T> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator false(Expected<T> result) => result.IsError;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> operator &(Expected<T> x, Expected<T> y) => x.And(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> operator |(Expected<T> x, Expected<T> y) => x.Or(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Expected<T> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T>(T value) => Okay(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T>(Success<T> success) => Okay(success.Value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T>(Error error) => Fail(error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T>(Failure<Error> failure) => Fail(failure.Error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Expected<T>(Result<T, Error> result) => new(result);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, Error>(Expected<T> result) => result.Match(Result<T, Error>.Okay, Result<T, Error>.Fail);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator T(Expected<T> result)
            => result.SelectError(err => new InvalidCastException($"Cannot cast a faulted result to a value", err.AsException()))
                     .Unwrap();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Error(Expected<T> result)
            => result is Expected.Failure<T>(var err)
                   ? err : throw new InvalidCastException("Cannot cast a successful result to an error");
    }
}
