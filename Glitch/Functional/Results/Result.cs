using Glitch.Functional.Attributes;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    [Monad]
    public abstract partial record Result<T, E>
    {
        private protected Result() { }

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, E> Okay(T value) => new Result.Success<T, E>(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, E> Fail(E error) => new Result.Failure<T, E>(error);

        public abstract bool IsOkay { get; }

        public abstract bool IsError { get; }

        /// <summary>
        /// If the result is <see cref="Result.Success{T}" />, applies
        /// the provided function to the value and returns it wrapped in a
        /// new <see cref="Expected{T}" />. Otherwise, returns the current error
        /// wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract Result<TResult, E> Select<TResult>(Func<T, TResult> map);

        public Result<Func<T2, TResult>, E> PartialSelect<T2, TResult>(Func<T, T2, TResult> map)
            => Select(map.Curry());

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Result<T, TNewError> SelectError<TNewError>(Func<E, TNewError> map);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns a faulted <see cref="Expected{TResult}" /> containing the 
        /// error value of self if it exists or the error value of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TResult, E> Apply<TResult>(Result<Func<T, TResult>, E> function)
            => AndThen(v => function.Select(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Result<TResult, E> And<TResult>(Result<TResult, E> other);

        public Result<TResult, E> And<TResult>(Success<TResult> other)
            => And(Result.Okay<TResult, E>(other.Value));

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> bind);

        /// <summary>
        /// BindMap operation, similar to the two arg overload of SelectMany.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<TResult, E> AndThen<TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));

        /// <summary>
        /// Returns the current result if Ok, otherwise returns the other result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Result<T, EResult> Or<EResult>(Result<T, EResult> other);

        /// <summary>
        /// Returns the current result if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> other);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Impure actions should really be Effects.")]
        public Result<T, E> Do(Action<T> action) => Do(action.Return());

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Impure actions should really be Effects.")]
        public Result<T, E> Do(Func<T, Unit> action) => Select(x => action(x).Return(x));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Impure actions should really be Effects.")]
        public Result<T, E> IfFail(Action action) => IfFail(_ => action());

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Impure actions should really be Effects.")]
        public abstract Result<T, E> IfFail(Action<E> action);

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
        public Result<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        public Result<E, T> Flip() => Match(Result.Fail<E, T>, Result.Okay<E, T>);

        /// <summary>
        /// For a successful result, checks the value against a predicate
        /// and returns a the provided <paramref name="error"/> if it fails.
        /// Does nothing for a failed result.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Result<T, E> Guard(Func<T, bool> predicate, E error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> Guard(bool condition, E error)
            => Guard(_ => condition, error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> Guard(bool condition, Func<T, E> error)
            => Guard(_ => condition, error);

        /// <summary>
        /// Combines another result into a result of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<(T, TOther), E> Zip<TOther>(Result<TOther, E> other)
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
        public Result<TResult, E> Zip<TOther, TResult>(Result<TOther, E> other, Func<T, TOther, TResult> zipper)
            => AndThen(_ => other, zipper);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract override string ToString();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Result<T, E> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator false(Result<T, E> result) => result.IsError;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, E> operator &(Result<T, E> x, Result<T, E> y) => x.And(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<T, E> operator |(Result<T, E> x, Result<T, E> y) => x.Or(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Result<T, E> result) => result.IsOkay;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(T value) => Okay(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(Success<T> success) => Okay(success.Value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(E error) => Fail(error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<T, E>(Failure<E> failure) => Fail(failure.Error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator T(Result<T, E> result)
            => result.Match(Identity, _ => Errors.InvalidCast<T>(result).Throw<T>());

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator E(Result<T, E> result)
            => result.Match(_ => Errors.InvalidCast<E>(result).Throw<E>(), Identity);
    }
}