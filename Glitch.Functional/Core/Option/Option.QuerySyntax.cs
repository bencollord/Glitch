namespace Glitch.Functional;

public partial struct Option<T>
{
    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project) => IsSome ? project(value!, bind(value!).value!) : Option<TResult>.None; // Code is duplicated here on purpose since pass throughs here get annoying in the debugger.
    public Option<TResult> SelectMany<TElement, TResult>(Func<T, Okay<TElement>> bind, Func<T, TElement, TResult> project) => IsSome ? project(value!, bind(value!).Value) : Option<TResult>.None;
}
