using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects;

public static partial class EffectExtensions
{
    public static Effect<TResult> SelectMany<T, TElement, TResult>(this Effect<T> source, Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> bindMap)
        => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

    public static Effect<TResult> SelectMany<T, TElement, TResult>(this Effect<T> source, Func<T, Result<TElement, Error>> bind, Func<T, TElement, TResult> bindMap)
        => source.AndThen(s => Effect.Return(bind(s).Select(e => bindMap(s, e))));

    public static Effect<TResult> SelectMany<T, E, TResult>(this Effect<T> source, Func<T, Guard<Unit, E>> bind, Func<T, Unit, TResult> bindMap)
        where E : Error
        => source.AndThen(bind, bindMap);

    public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<T> source, Func<T, Effect<TEnv, TElement>> bind, Func<T, TElement, TResult> bindMap)
        => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));
}
