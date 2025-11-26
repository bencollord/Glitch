using Glitch.Functional;

namespace Glitch.Functional.Errors;

public partial record Expected<T> : IResult<T, Error>
{
    private Result<T, Error> inner;

    // Prevent deriving from outside the type.
    private Expected(Result<T, Error> inner)
    {
        this.inner = inner;
    }

    public static Expected<T> From<E>(Result<T, E> result)
        where E : Error
        => new(result.SelectError(StaticCast<Error>.UpFrom));

    public bool IsOkay => inner.IsOkay;

    public bool IsFail => inner.IsFail;

    /// <summary>
    /// Retypes the instance as a <see cref="Result{T, Error}"/>.
    /// </summary>
    /// <returns></returns>
    public Result<T, Error> AsResult()
    {
        return inner;
    }

    /// <summary>
    /// If the result is <see cref="Okay{T}" />, applies
    /// the provided function to the value and returns it wrapped in a
    /// new <see cref="Expected{T}" />. Otherwise, returns the current error
    /// wrapped in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="map"></param>
    /// <returns></returns>
    public Expected<TResult> Select<TResult>(Func<T, TResult> map) => new(inner.Select(map));

    /// <summary>
    /// If the result is a failure, returns a new result with the mapping function
    /// applied to the wrapped error. Otherwise, returns self.
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    public Expected<T> SelectError(Func<Error, Error> map) => inner.SelectError(e => map(e));

    public Expected<T> SelectError(Func<Error, Exception> map) => SelectError(e => Error.New(map(e)));

    /// <summary>
    /// If the result is a failure, returns a new result with the mapping function
    /// applied to the wrapped error. Otherwise, returns self.
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    public Result<T, E> SelectError<E>(Func<Error, E> map) => inner.SelectError(e => map(e));

    /// <summary>
    /// Applies a wrapped function to the wrapped value if both exist.
    /// Otherwise, returns a faulted <see cref="Expected{TResult}" /> containing the 
    /// error value of self if it exists or the error value of <paramref name="function"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public Expected<TResult> Apply<TResult>(Expected<Func<T, TResult>> function)
        => AndThen(v => function.Select(fn => fn(v)));

    /// <summary>
    /// Returns other if Ok, otherwise returns the current error wrapped
    /// in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Expected<TResult> And<TResult>(Expected<TResult> other) => IsOkay ? other : Cast<TResult>();

    /// <summary>
    /// If Okay, applies the function to the wrapped value. Otherwise, returns
    /// the current error wrapped in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <returns></returns>
    public Expected<TResult> AndThen<TResult>(Func<T, Expected<TResult>> bind)
        => inner.AndThen(x => bind(x).inner);

    /// <summary>
    /// If Okay, applies the function to the wrapped value. Otherwise, returns
    /// the current error wrapped in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <returns></returns>
    public Expected<TResult> AndThen<TResult>(Func<T, Result<TResult, Error>> bind)
        => inner.AndThen(x => bind(x));

    /// <summary>
    /// BindMap operation, similar to the two arg overload of SelectMany.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <param name="project"></param>
    /// <returns></returns>
    public Expected<TResult> AndThen<TElement, TResult>(Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(y => project(x, y)));

    /// <summary>
    /// Returns the current result if Ok, otherwise returns the other result.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Expected<T> Or(Expected<T> other) => new(inner.Or(other.inner));

    /// <summary>
    /// Returns the current result if Ok, otherwise applies the provided
    /// function to the current error and returns the result.
    /// </summary>
    /// <param name="bind"></param>
    /// <returns></returns>
    public Expected<T> OrElse(Func<Error, Expected<T>> bind) => inner.OrElse(x => bind(x).inner);

    public T IfFail(T fallback) => inner.IfFail(fallback);

    public T IfFail(Func<Error, T> fallback) => inner.IfFail(fallback);

    /// <summary>
    /// If Ok, returns the result of the first function to the wrapped value.
    /// Otherwise, returns the result of the second function to the wrapped error.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public TResult Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> fail)
        => inner.Match(okay, fail);

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
    public Expected<TResult> Cast<TResult>() =>
        AndThen(x => DynamicCast<TResult>.Try(x)
            .Match(some: Expected.Okay, 
                   none: Error.InvalidCast<TResult>(x)));

    public Expected<T> Where(Func<T, bool> predicate) => Guard(predicate, Error.Empty);

    /// <summary>
    /// For a successful result, checks the value against a predicate
    /// and returns a the provided <paramref name="error"/> if it fails.
    /// Does nothing for a failed result.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Expected<T> Guard(Func<T, bool> predicate, Error error)
        => inner.Guard(predicate, error);

    public Expected<T> Guard(Func<T, bool> predicate, Func<T, Error> error)
        => inner.Guard(predicate, error);

    public Expected<T> Guard(bool condition, Error error)
        => inner.Guard(condition, error);

    public Expected<T> Guard(bool condition, Func<T, Error> error)
        => inner.Guard(condition, error);

    /// <summary>
    /// Combines another result into a result of a tuple.
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
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
    /// <param name="zip"></param>
    /// <returns></returns>
    public Expected<TResult> Zip<TOther, TResult>(Expected<TOther> other, Func<T, TOther, TResult> zip)
        => AndThen(_ => other, zip);

    /// <summary>
    /// Returns the wrapped value if ok. Otherwise throws the wrapped error
    /// as an exception.
    /// </summary>
    /// <remarks>
    /// Overrides the default implementation provided by <see cref="IResult{T, E}"/>
    /// to ensure the exception thrown is the contained <see cref="Error"/> value.
    /// </remarks>
    /// <returns></returns>
    public T Unwrap() => inner.IfFail(err => err.Throw<T>());

    public Error UnwrapError() => inner.UnwrapError();

    public override string ToString() => inner.ToString();
}
