using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

using static FN;

/// <summary>
/// A validation monad. Equivalent to <see cref="Result{T, E}"/>, but accumulates failures
/// into a <see cref="Sequence{E}"/> instead of short-circuiting on failure.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="E"></typeparam>
[Monad] // UNDONE Need to actually collect and sequence errors.
public abstract partial record Validated<T, E> : IResult<T, Sequence<E>>
{
    private protected Validated() { }

    public abstract bool IsOkay { get; }

    public abstract bool IsFail { get; }

    public abstract Validated<TResult, E> Select<TResult>(Func<T, TResult> map);

    public abstract Validated<T, EResult> SelectError<EResult>(Func<E, EResult> map);

    public abstract Validated<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> fail);

    /// <summary>
    /// Applies a wrapped function to the wrapped value if both exist.
    /// Otherwise, returns a faulted <see cref="Validated{TResult, E}" /> containing the 
    /// error value of self if it exists or the error value of <paramref name="function"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public Validated<TResult, E> Apply<TResult>(Validated<Func<T, TResult>, E> function)
        => AndThen(v => function.Select(fn => fn(v)));

    /// <summary>
    /// Returns other if Ok, otherwise returns the current error wrapped
    /// in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Validated<TResult, E> And<TResult>(Validated<TResult, E> other);

    public Validated<TResult, E> And<TResult>(Okay<TResult> other)
        => And(Validated.Okay<TResult, E>(other.Value));

    /// <summary>
    /// If Okay, applies the function to the wrapped value. Otherwise, returns
    /// the current error wrapped in a new result type.
    /// </summary>
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
    /// Returns the current result if Ok, otherwise returns the other result.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Validated<T, EResult> Or<EResult>(Validated<T, EResult> other);

    /// <summary>
    /// Returns the current result if Ok, otherwise applies the provided
    /// function to the current error and returns the result.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Validated<T, EResult> OrElse<EResult>(Func<Sequence<E>, Validated<T, EResult>> other);

    public TResult Match<TResult>(Func<T, TResult> okay, TResult fail) => Match(okay, _ => fail);

    /// <summary>
    /// If Ok, returns the result of the first function to the wrapped value.
    /// Otherwise, returns the result of the second function to the wrapped error.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="fail"></param>
    /// <returns></returns>
    public abstract TResult Match<TResult>(Func<T, TResult> okay, Func<Sequence<E>, TResult> fail);

    public T IfFail(Func<Sequence<E>, T> fallback) => Match(Identity, fallback);

    public T IfFail(T fallback) => Match(Identity, fallback);

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
    public Validated<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);

    /// <summary>
    /// For a successful result, checks the value against a predicate
    /// and returns a the provided <paramref name="error"/> if it fails.
    /// Does nothing for a failed result.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public abstract Validated<T, E> Guard(Func<T, bool> predicate, E error);

    public abstract Validated<T, E> Guard(Func<T, bool> predicate, Func<T, E> error);

    public Validated<T, E> Guard(bool condition, E error)
        => Guard(_ => condition, error);

    public Validated<T, E> Guard(bool condition, Func<T, E> error)
        => Guard(_ => condition, error);

    /// <summary>
    /// Combines another result into a result of a tuple.
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Validated<(T, TOther), E> Zip<TOther>(Validated<TOther, E> other)
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
    public abstract Validated<TResult, E> Zip<TOther, TResult>(Validated<TOther, E> other, Func<T, TOther, TResult> zipper);

    public abstract override string ToString();
}