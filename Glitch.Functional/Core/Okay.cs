namespace Glitch.Functional;

public readonly record struct Okay<T>(T Value)
{
    public Result<T, E> ToResult<E>() => Result.Okay<T, E>(Value);

    // Linq
    public Okay<TResult> Select<TResult>(Func<T, TResult> selector) => new(selector(Value));

    public Result<TResult, E> SelectMany<E, TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
        => bind(Value).Select(project.Curry(Value));

    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
       => bind(Value).Select(project.Curry(Value));

    public override string ToString() => $"Okay({Value})";

    public static implicit operator Okay<T>(T value) => new(value);

    public static implicit operator T(Okay<T> success) => success.Value;
}
