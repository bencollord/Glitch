namespace Glitch.Functional;

public readonly record struct Okay<T>(T Value)
{
    public Result<T, E> ToResult<E>() => Result.Okay<T, E>(Value);

    public Option<T> ToOption() => Option.Some(Value);

    // Linq
    public Okay<TResult> Select<TResult>(Func<T, TResult> selector) => new(selector(Value));

    public Result<T, E> AndThen<E>(Func<T, Fail<E>> bind) => bind(Value);

    public Result<TResult, E> AndThen<E, TResult>(Func<T, Result<TResult, E>> bind) => bind(Value);

    public Option<TResult> AndThen<TResult>(Func<T, Option<TResult>> bind) => bind(Value);

    public Result<TResult, E> AndThen<E, TResult>(Func<T, Fail<E>> bind, Func<T, Unit, TResult> _) => bind(Value);

    public Result<TResult, E> AndThen<E, TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
        => bind(Value).Select(project.Curry(Value));

    public Option<TResult> AndThen<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
       => bind(Value).Select(project.Curry(Value));

    public Result<TResult, E> SelectMany<E, TResult>(Func<T, Fail<E>> bind, Func<T, Unit, TResult> _) => bind(Value);

    public Result<TResult, E> SelectMany<E, TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
        => bind(Value).Select(project.Curry(Value));

    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
       => bind(Value).Select(project.Curry(Value));

    public override string ToString() => $"Okay({Value})";

    public static implicit operator Okay<T>(T value) => new(value);

    public static implicit operator T(Okay<T> success) => success.Value;
}
