
namespace Glitch.Functional;

public partial record Result<T>
{
    public Result<TResult> SelectMany<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Result<TResult> SelectMany<TElement, E, TResult>(Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project) where E : Error => AndThen(x => Result.From(bind(x)), project);
    public Result<TResult> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(x => Result.Okay(bind(x).Value), project);
}
