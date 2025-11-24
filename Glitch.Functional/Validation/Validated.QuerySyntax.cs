namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    public Validated<TResult, E> SelectMany<TElement, TResult>(Func<T, Validated<TElement, E>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Validated<TResult, E> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(x => Validated.Okay<TElement, E>(bind(x).Value), project);
}
