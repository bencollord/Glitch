using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Effects;

public static partial class EffectExtensions
{
    // Effect w/env
    public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Effect<TEnv, TElement>> bind, Func<T, TElement, TResult> bindMap)
        => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

    // Effect no env
    public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> bindMap)
        => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

    // Result
    public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> bindMap)
        => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

    // Expected
    public static Effect<TEnv, TResult> SelectMany<TEnv, T, E, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> bindMap)
        where E : Error
        => source.AndThen(s => Effect.Return(bind(s)).Select(e => bindMap(s, e)));

    // Option
    // UNDONE Result -> Expected conversion
    //public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> bindMap)
    //    => source.AndThen(s => bind(s).OkayOr(Error.Empty).Select(e => bindMap(s, e)));
}
