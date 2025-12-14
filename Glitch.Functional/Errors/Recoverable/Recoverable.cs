namespace Glitch.Functional.Errors;

using static FN;

/// <summary>
/// A result monad with a notion of non-fatal error. Equivalent to <see cref="Result{T, E}"/>, but
/// with an added case for warnings, which have a value and an error.
/// 
/// Can be in three states:
/// Okay: (has value, no error)
/// Warning: (has value and error)
/// Fatal: (has error, no value).
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="E"></typeparam>
[Monad]
public abstract partial record Recoverable<T, E> : IMaybe<T>
{
    private protected Recoverable() { }

    public abstract bool HasValue { get; }

    public abstract bool HasError { get; }

    public bool IsOkay => HasValue && !HasError;

    public bool IsWarning => HasValue && HasError;
    
    public bool IsFatal => !HasValue && HasError;

    /// <summary>
    /// Maps the success value if okay or warning.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="map"></param>
    /// <returns></returns>
    public abstract Recoverable<TResult, E> Select<TResult>(Func<T, TResult> map);

    /// <summary>
    /// Maps the error values if has error.
    /// </summary>
    /// <typeparam name="EResult"></typeparam>
    /// <param name="map"></param>
    /// <returns></returns>
    public abstract Recoverable<T, EResult> SelectError<EResult>(Func<E, EResult> map);

    /// <summary>
    /// Maps both the success and error values and returns either/or
    /// wrapped in a new <see cref="Recoverable{T, E}"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="EResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="fail"></param>
    /// <returns></returns>
    public abstract Recoverable<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> fail);

    /// <summary>
    /// Applies a wrapped function to the wrapped value if both have values.
    /// Otherwise, returns a faulted <see cref="Recoverable{TResult, E}" />,
    /// containing the first fatal error or the last warning value.
    /// 
    /// If both are warnings, combines the error.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public abstract Recoverable<TResult, E> Apply<TResult>(Recoverable<Func<T, TResult>, E> function);

    /// <summary>
    /// Returns other if Okay, or whichever error was fatal. If both are warnings, 
    /// returns the error value of other.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Recoverable<TResult, E> And<TResult>(Recoverable<TResult, E> other);

    /// <summary>
    /// Returns <paramref name="other"/> wrapped in a <see cref="Recoverable{T, E}"/> if okay.
    /// Otherwise, returns the error value of self.
    /// 
    /// If warning, returns other with this instance's error value.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Recoverable<TResult, E> And<TResult>(Okay<TResult> other)
        => And(Recoverable.Okay<TResult, E>(other.Value));

    /// <summary>
    /// If Okay, applies the function to the wrapped value. Otherwise, returns
    /// the current error set retyped to <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <returns></returns>
    public abstract Recoverable<TResult, E> AndThen<TResult>(Func<T, Recoverable<TResult, E>> bind);

    /// <summary>
    /// BindMap operation, similar to the two arg overload of SelectMany.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <param name="project"></param>
    /// <returns></returns>
    public Recoverable<TResult, E> AndThen<TElement, TResult>(Func<T, Recoverable<TElement, E>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(y => project(x, y)));

    /// <summary>
    /// If Okay, returns this. If <paramref name="other"/> is Okay, <paramref name="other"/>.
    /// If both fail, returns the first fatal error or the last warning.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Recoverable<T, E> Or(Recoverable<T, E> other);

    /// <summary>
    /// Returns the current result if Okay, otherwise applies the provided
    /// function to the current error set and if the result also fails,
    /// returns the combined error.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Recoverable<T, E> OrElse(Func<E, Recoverable<T, E>> other);

    /// <summary>
    /// If Okay, returns <paramref name="okay"/> applied to the wrapped value.
    /// Otherwise, returns <paramref name="fatal"/> applied to the wrapped error.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="fatal"></param>
    /// <returns></returns>
    public abstract TResult Match<TResult>(Func<T, TResult> okay, Func<T, E, TResult> warning, Func<E, TResult> fatal);

    public TResult Merge<TResult>(Func<T, TResult> okay, Func<E, TResult> error, Func<TResult, TResult, TResult> merge) =>
        Match(okay: okay, fatal: error, warning: (v, e) => merge(okay(v), error(e)));

    /// <summary>
    /// Unwraps the <typeparamref name="T"/> value, or returns the result of <paramref name="fallback"/>.
    /// </summary>
    /// <param name="fallback"></param>
    /// <returns></returns>
    public T IfFatal(Func<E, T> fallback) => Match(Identity, (v, e) => v, fallback);

    /// <summary>
    /// Unwraps the <typeparamref name="T"/> value, or returns <paramref name="fallback"/>.
    /// </summary>
    /// <param name="fallback"></param>
    /// <returns></returns>
    public abstract T IfFatal(T fallback);

    /// <summary>
    /// If Okay, casts the wrapped value to <typeparamref name="TResult"/>,
    /// otherwise returns the current error wrapped in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <exception cref="InvalidCastException">
    /// If the cast is not valid.
    /// </exception>
    /// <returns></returns>
    public Recoverable<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);

    /// <summary>
    /// If has error, casts the wrapped error to <typeparamref name="EResult"/>,
    /// otherwise returns the current value wrapped in a new result type.
    /// </summary>
    /// <typeparam name="EResult"></typeparam>
    /// <exception cref="InvalidCastException">
    /// If the cast is not valid.
    /// </exception>
    /// <returns></returns>
    public Recoverable<T, EResult> CastError<EResult>() => SelectError(DynamicCast<EResult>.From);

    /// <summary>
    /// Combines self and <paramref name="other"/> into a validation of a tuple.
    /// If both validations have error, combines the error.
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Recoverable<(T, TOther), E> Zip<TOther>(Recoverable<TOther, E> other)
        => Zip(other, (x, y) => (x, y));

    /// <summary>
    /// Combines two results using a provided function if both are okay.
    /// Otherwise, returns the error value of whichever one failed.
    /// If both have error, combines the error.
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <param name="zipper"></param>
    /// <returns></returns>
    public abstract Recoverable<TResult, E> Zip<TOther, TResult>(Recoverable<TOther, E> other, Func<T, TOther, TResult> zipper);

    /// <summary>
    /// Returns a string representing this <see cref="Recoverable{T, E}"/>.
    /// If fail, error are combined into a comma separated list.
    /// </summary>
    /// <returns></returns>
    public abstract override string ToString();

    TResult IMaybe<T>.Match<TResult>(Func<T, TResult> some, Func<TResult> none) =>
        Match(some, (v, e) => some(v), _ => none());
}