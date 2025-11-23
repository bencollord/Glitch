namespace Glitch.Functional.Errors;

public partial record Expected<T>
{
    public Expected<TResult> SelectMany<TElement, TResult>(Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Expected<TResult> SelectMany<TElement, E, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project) where E : Error => AndThen(x => Expected.From(bind(x)), project);
    public Expected<TResult> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(x => Expected.Okay(bind(x).Value), project);
}
