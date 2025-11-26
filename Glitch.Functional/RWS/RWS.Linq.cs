namespace Glitch.Functional;

public partial class RWS<TEnv, S, W, T>
    where W : IWritable<W>
{
    public RWS<TEnv, S, W, TResult> SelectMany<TElement, TResult>(Func<T, RWS<TEnv, S, W, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(bind, project);

    public RWS<TEnv, S, W, TResult> SelectMany<TElement, TResult>(Func<T, Reader<TEnv, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(bind, project);

    public RWS<TEnv, S, W, TResult> SelectMany<TElement, TResult>(Func<T, Writer<W, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(bind, project);

    public RWS<TEnv, S, W, TResult> SelectMany<TElement, TResult>(Func<T, IStateful<S, TElement>> bind, Func<T, TElement, TResult> project)
        => AndThen(bind, project);
}
