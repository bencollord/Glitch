namespace Glitch.Functional;

public partial struct Option<T>
{
    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project) => AndThen(bind, project);
    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => Select(x => project(x, bind(x).Value));
}
