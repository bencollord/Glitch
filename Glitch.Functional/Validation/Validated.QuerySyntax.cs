namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    public Validated<TResult, E> SelectMany<TElement, TResult>(Func<T, Validated<TElement, E>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Validated<TResult, E> SelectMany<TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Validated<TResult, E> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Validated<TResult, E> SelectMany<TResult>(Func<T, Fail<E>> bind, Func<T, Unit, TResult> project) => AndThen(bind, project);
}
