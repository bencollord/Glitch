using Glitch.Functional.Validation.Recoverable;

namespace Glitch.Functional;

public partial record Recoverable<T, E>
{
    public Recoverable<TResult, E> SelectMany<TElement, TResult>(Func<T, Recoverable<TElement, E>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Recoverable<TResult, E> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(x => Recoverable.Okay<TElement, E>(bind(x).Value), project);
}
