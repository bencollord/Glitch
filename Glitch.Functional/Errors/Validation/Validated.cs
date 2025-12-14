using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

using static FN;

/// <summary>
/// A validation monad. Equivalent to <see cref="Result{T, E}"/>, but accumulates failures
/// into a <see cref="Sequence{E}"/> instead of short-circuiting on failure.
/// 
/// Can be in three states:
/// Okay: (has value, no errors)
/// Warning: (has value and errors)
/// Fatal: (has errors, no value).
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="E"></typeparam>
[Monad]
public abstract partial record Validated<T, E> : IMaybe<T>
{
    private protected Validated() { }

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
    public abstract Validated<TResult, E> Select<TResult>(Func<T, TResult> map);

    /// <summary>
    /// Maps the error values if has errors.
    /// </summary>
    /// <typeparam name="EResult"></typeparam>
    /// <param name="map"></param>
    /// <returns></returns>
    public abstract Validated<T, EResult> SelectError<EResult>(Func<E, EResult> map);

    /// <summary>
    /// Maps both the success and error values and returns either/or
    /// wrapped in a new <see cref="Validated{T, E}"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="EResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="fail"></param>
    /// <returns></returns>
    public abstract Validated<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> fail);

    /// <summary>
    /// Applies a wrapped function to the wrapped value if both have values.
    /// Otherwise, returns a faulted <see cref="Validated{TResult, E}" />,
    /// containing any errors in either.
    /// 
    /// If both are warnings, combines the errors.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public abstract Validated<TResult, E> Apply<TResult>(Validated<Func<T, TResult>, E> function);

    /// <summary>
    /// Returns other if Okay, otherwise returns a faulted result
    /// containing any errors found in either.
    /// 
    /// If both are warnings, returns other with this instances errors merged.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Validated<TResult, E> And<TResult>(Validated<TResult, E> other);

    /// <summary>
    /// Returns <paramref name="other"/> wrapped in a <see cref="Validated{T, E}"/> if okay.
    /// Otherwise, returns the error set of self.
    /// 
    /// If warning, returns other with this instance's error log.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Validated<TResult, E> And<TResult>(Okay<TResult> other)
        => And(Validated.Okay<TResult, E>(other.Value));

    /// <summary>
    /// If Okay, applies the function to the wrapped value. Otherwise, returns
    /// the current error set retyped to <typeparamref name="TResult"/>.
    /// </summary>
    /// <remarks>
    /// Since a success value is required to run the <paramref name="bind"/> function,
    /// failed values will not be aggregated, which is important to remember when using
    /// Linq query syntax. If this instance is a warning and thus has a value to return,
    /// errors will be aggregated.
    /// </remarks>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <returns></returns>
    public abstract Validated<TResult, E> AndThen<TResult>(Func<T, Validated<TResult, E>> bind);

    /// <summary>
    /// BindMap operation, similar to the two arg overload of SelectMany.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <param name="project"></param>
    /// <returns></returns>
    public Validated<TResult, E> AndThen<TElement, TResult>(Func<T, Validated<TElement, E>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(y => project(x, y)));

    /// <summary>
    /// If Okay, returns this. If <paramref name="other"/> is Okay, <paramref name="other"/>.
    /// If both fail, returns the combined errors.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Validated<T, E> Or(Validated<T, E> other);

    /// <summary>
    /// Returns the current result if Okay, otherwise applies the provided
    /// function to the current error set and if the result also fails,
    /// returns the combined errors.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Validated<T, E> OrElse(Func<Sequence<E>, Validated<T, E>> other);

    /// <summary>
    /// If Okay, returns <paramref name="okay"/> applied to the wrapped value.
    /// Otherwise, returns <paramref name="fatal"/> applied to the wrapped errors.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="fatal"></param>
    /// <returns></returns>
    public abstract TResult Match<TResult>(Func<T, TResult> okay, Func<T, Sequence<E>, TResult> warning, Func<Sequence<E>, TResult> fatal);

    public TResult Merge<TResult>(Func<T, TResult> okay, Func<Sequence<E>, TResult> error, Func<TResult, TResult, TResult> merge) =>
        Match(okay: okay, fatal: error, warning: (v, e) => merge(okay(v), error(e)));

    /// <summary>
    /// Unwraps the <typeparamref name="T"/> value, or returns the result of <paramref name="fallback"/>.
    /// </summary>
    /// <param name="fallback"></param>
    /// <returns></returns>
    public T IfFatal(Func<Sequence<E>, T> fallback) => Match(Identity, (v, e) => v, fallback);

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
    public Validated<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);

    /// <summary>
    /// If has error, casts the wrapped errors to <typeparamref name="EResult"/>,
    /// otherwise returns the current value wrapped in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <exception cref="InvalidCastException">
    /// If the cast is not valid.
    /// </exception>
    /// <returns></returns>
    public Validated<T, EResult> CastError<EResult>() => SelectError(DynamicCast<EResult>.From);

    /// <summary>
    /// Combines self and <paramref name="other"/> into a validation of a tuple.
    /// If both validations have errors, combines the errors.
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Validated<(T, TOther), E> Zip<TOther>(Validated<TOther, E> other)
        => Zip(other, (x, y) => (x, y));

    /// <summary>
    /// Combines two results using a provided function if both are okay.
    /// Otherwise, returns the error value of whichever one failed.
    /// If both have errors, combines the errors.
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <param name="zipper"></param>
    /// <returns></returns>
    public abstract Validated<TResult, E> Zip<TOther, TResult>(Validated<TOther, E> other, Func<T, TOther, TResult> zipper);

    /// <summary>
    /// Returns a string representing this <see cref="Validated{T, E}"/>.
    /// If fail, errors are combined into a comma separated list.
    /// </summary>
    /// <returns></returns>
    public abstract override string ToString();

    T IMaybe<T>.UnwrapOrElse(Func<T> fallback) => IfFatal(_ => fallback());
}