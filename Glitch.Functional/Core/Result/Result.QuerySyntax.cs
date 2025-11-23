namespace Glitch.Functional;

public partial record Result<T, E>
{
    public Result<TResult, E> SelectMany<TElement, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Result<TResult, E> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(x => Result.Okay<TElement, E>(bind(x).Value), project);
}
