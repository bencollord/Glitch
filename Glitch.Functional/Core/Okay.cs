using Glitch.Functional.Validation;

namespace Glitch.Functional;

public readonly record struct Okay<T>(T Value)
{
    public Result<T, E> OrFail<E>() => Result.Okay<T, E>(Value);

    public Option<T> OrNone() => Option.Some(Value);

    // Linq
    // ============================================================================================

    // Map
    // --------------------------------------------------------------------------------------------
    public Okay<TResult> Cast<TResult>() => DynamicCast<TResult>.From(Value); // TODO Should this return a result?
    public Okay<TResult> Select<TResult>(Func<T, TResult> selector) => new(selector(Value));

    // Bind
    // --------------------------------------------------------------------------------------------
    public Okay<TResult> AndThen<TResult>(Func<T, Okay<TResult>> bind) => bind(Value);
    public Okay<TResult> AndThen<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => project(Value, bind(Value).Value);
    public Okay<TResult> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => project(Value, bind(Value).Value);

    // Fail
    public Result<T, E> AndThen<E>(Func<T, Fail<E>> bind) => bind(Value);
    public Result<TResult, E> AndThen<E, TResult>(Func<T, Fail<E>> bind, Func<T, Unit, TResult> _) => bind(Value);
    public Result<TResult, E> SelectMany<E, TResult>(Func<T, Fail<E>> bind, Func<T, Unit, TResult> _) => bind(Value);

    // Option
    public Option<TResult> AndThen<TResult>(Func<T, Option<TResult>> bind) => bind(Value);
    public Option<TResult> AndThen<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
       => bind(Value).Select(project.Curry(Value));
    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
       => bind(Value).Select(project.Curry(Value));

    // Result
    public Result<TResult, E> AndThen<E, TResult>(Func<T, Result<TResult, E>> bind) => bind(Value);
    public Result<TResult, E> AndThen<E, TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
        => bind(Value).Select(project.Curry(Value));
    public Result<TResult, E> SelectMany<E, TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
        => bind(Value).Select(project.Curry(Value));

    // Validated
    public Validated<TResult, E> AndThen<E, TResult>(Func<T, Validated<TResult, E>> bind) => bind(Value);
    public Validated<TResult, E> AndThen<E, TElement, TResult>(Func<T, Validated<TElement, E>> bind, Func<T, TElement, TResult> project)
        => bind(Value).Select(project.Curry(Value));
    public Validated<TResult, E> SelectMany<E, TElement, TResult>(Func<T, Validated<TElement, E>> bind, Func<T, TElement, TResult> project)
        => bind(Value).Select(project.Curry(Value));

    public override string ToString() => $"Okay({Value})";

    public static implicit operator Okay<T>(T value) => new(value);

    public static implicit operator T(Okay<T> success) => success.Value;
}
