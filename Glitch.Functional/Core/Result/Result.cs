namespace Glitch.Functional;

public readonly union Result<T, E>(Okay<T>, Fail<E>) : IResult<T, E>, IMaybe<T>
{
    public readonly bool IsOkay => this is Okay<T>;

    public readonly bool IsFail => this is Fail<E>;

    bool IMaybe.HasValue => IsOkay;

    /// <summary>
    /// If the result is <see cref="Result{T, E}.Okay" />, applies
    /// the provided function to the value and returns it wrapped in a
    /// new <see cref="Result{T, E}" />. Otherwise, returns the current error
    /// wrapped in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="map"></param>
    /// <returns></returns>
    public Result<TResult, E> Select<TResult>(Func<T, TResult> map) =>
        this switch
        {
            Okay<T>(var val) => map(val),
            Fail<E> err => err
        };

    /// <summary>
    /// If the result is a failure, returns a new result with the mapping function
    /// applied to the wrapped error. Otherwise, returns self.
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    public Result<T, EResult> SelectError<EResult>(Func<E, EResult> map) =>
        this switch
        {
            Okay<T> ok => ok,
            Fail<E>(var err) => map(err)
        };

    /// <summary>
    /// Bifunctor map operation. 
    /// If Okay, equivalent to <see cref="Select{TResult}(Func{T, TResult})"/> plus <see cref="CastError{EResult}"/>.
    /// If Fail, equivalent to <see cref="SelectError{EResult}(Func{E, EResult})"/> plus <see cref="Cast{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="EResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="fail"></param>
    /// <returns></returns>
    public Result<TResult, EResult> BiSelect<TResult, EResult>(Func<T, TResult> okay, Func<E, EResult> fail) =>
        this switch
        {
            Okay<T>(var val) => okay(val),
            Fail<E>(var err) => fail(err)
        };

    /// <summary>
    /// Applies a wrapped function to the wrapped value if both exist.
    /// Otherwise, returns a faulted <see cref="Expected{TResult}" /> containing the 
    /// error value of self if it exists or the error value of <paramref name="function"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public Result<TResult, E> Apply<TResult>(Result<Func<T, TResult>, E> function)
        => AndThen(v => function.Select(fn => fn(v)));

    /// <summary>
    /// Returns other if Ok, otherwise returns the current error wrapped
    /// in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Result<TResult, E> And<TResult>(Result<TResult, E> other) =>
        this switch
        {
            Okay<T> => other,
            Fail<E> err => err
        };

    /// <summary>
    /// If Okay, applies the function to the wrapped value. Otherwise, returns
    /// the current error wrapped in a new result type.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <returns></returns>
    public Result<TResult, E> AndThen<TResult>(Func<T, Result<TResult, E>> bind) =>
        this switch
        {
            Okay<T>(var val) => bind(val),
            Fail<E> err => err
        };

    /// <summary>
    /// BindMap operation, similar to the two arg overload of SelectMany.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="bind"></param>
    /// <param name="project"></param>
    /// <returns></returns>
    public Result<TResult, E> AndThen<TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
        => AndThen(x => bind(x).Select(y => project(x, y)));

    public Result<TResult, E> SelectMany<TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);

    /// <summary>
    /// Returns the current result if Ok, otherwise returns the other result.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Result<T, EResult> Or<EResult>(Result<T, EResult> other) =>
        this switch
        {
            Okay<T> ok => ok,
            Fail<E> => other
        };

    /// <summary>
    /// Returns the current result if Ok, otherwise applies the provided
    /// function to the current error and returns the result.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Result<T, EResult> OrElse<EResult>(Func<E, Result<T, EResult>> other) =>
        this switch
        {
            Okay<T> ok => ok,
            Fail<E>(var err) => other(err)
        };

    public TResult Match<TResult>(Func<T, TResult> okay, TResult fail) => Match(okay, _ => fail);

    /// <summary>
    /// If Ok, returns the result of the first function to the wrapped value.
    /// Otherwise, returns the result of the second function to the wrapped error.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="okay"></param>
    /// <param name="fail"></param>
    /// <returns></returns>
    public TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> fail) =>
        this switch
        {
            Okay<T>(var val) => okay(val),
            Fail<E>(var err) => fail(err)
        };

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
    public Result<TResult, E> Cast<TResult>() => Select(DynamicCast<TResult>.From);

    /// <summary>
    /// Casts the wrapped value to <typeparamref name="TResult"/>.
    /// If the cast fails, returns a <see cref="Result{TResult, E}.Fail"/>
    /// with the provided <paramref name="error"/>.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="error"></param>
    /// <returns></returns>
    public Result<TResult, E> CastOr<TResult>(E error) =>
        AndThen(x => DynamicCast<TResult>.Try(x).OkayOr(error));

    public T IfFail(Func<E, T> fallback) => Match(Identity, fallback);

    public T IfFail(T fallback) => Match(Identity, fallback);

    /// <summary>
    /// Casts the wrapped value to <typeparamref name="TResult"/>.
    /// If the cast fails, returns a <see cref="Result{TResult, E}.Fail"/>
    /// with the result provided <paramref name="error"/> function applied to 
    /// the wrapped value of this result.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="error"></param>
    /// <returns></returns>
    public Result<TResult, E> CastOrElse<TResult>(Func<T, E> error) =>
        AndThen(x => DynamicCast<TResult>.Try(x).OkayOrElse(_ => error(x)));

    /// <summary>
    /// If Fail, casts the wrapped error to <typeparamref name="EResult"/>,
    /// otherwise returns the current value wrapped in a new result type.
    /// </summary>
    /// <typeparam name="EResult"></typeparam>
    /// <exception cref="InvalidCastException">
    /// If the cast is not valid.
    /// </exception>
    /// <returns></returns>
    public Result<T, EResult> CastError<EResult>() => SelectError(DynamicCast<EResult>.From);

    public Result<E, T> Flip() => Match(Result.Fail<E, T>, Result.Okay<E, T>);

    /// <summary>
    /// For a successful result, checks the value against a predicate
    /// and returns a the provided <paramref name="error"/> if it fails.
    /// Does nothing for a failed result.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Result<T, E> Guard(Func<T, bool> predicate, E error) =>
        this switch
        {
            Okay<T>(var val) when !predicate(val) => error,
            _ => this
        };

    public Result<T, E> Guard(Func<T, bool> predicate, Func<T, E> error) =>
        this switch
        {
            Okay<T>(var val) when !predicate(val) => error(val),
            _ => this
        };

    public Result<T, E> Guard(bool condition, E error) => Guard(_ => condition, error);

    public Result<T, E> Guard(bool condition, Func<T, E> error) => Guard(_ => condition, error);

    /// <summary>
    /// Combines another result into a result of a tuple.
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public Result<(T, TOther), E> Zip<TOther>(Result<TOther, E> other) => Zip(other, (x, y) => (x, y));

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
    public Result<TResult, E> Zip<TOther, TResult>(Result<TOther, E> other, Func<T, TOther, TResult> zipper) => AndThen(_ => other, zipper);

    public override string ToString() =>
        this switch
        {
            Okay<T>(var val) => $"Okay({val})",
            Fail<E>(var err) => $"Fail({err})",
        };

    TResult IMaybe<T>.Match<TResult>(Func<T, TResult> some, Func<TResult> none) => Match(some, _ => none());

    public static bool operator true(Result<T, E> result) => result.IsOkay;

    public static bool operator false(Result<T, E> result) => result.IsFail;

    public static implicit operator Result<T, E>(T value) => new Okay<T>(value);

    public static implicit operator Result<T, E>(E error) => new Fail<E>(error);

    public static explicit operator T(Result<T, E> result)
        => result.Match(Identity, e => throw new InvalidCastException(ErrorMessages.InvalidCast<T>(e)));

    public static explicit operator E(Result<T, E> result)
        => result.Match(v => throw new InvalidCastException(ErrorMessages.InvalidCast<E>(result)), Identity);
}