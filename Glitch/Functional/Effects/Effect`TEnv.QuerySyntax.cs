using Glitch.Functional.Attributes;
using Glitch.Functional.Results;

namespace Glitch.Functional
{
    [MonadExtension(typeof(Effect<,>))]
    public static partial class EffectExtensions
    {
        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TResult>(this Effect<TEnv, T> source, Func<T, Effect<TEnv, TResult>> bind)
            => source.AndThen(bind);

        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Effect<TEnv, TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TResult>(this Effect<TEnv, T> source, Func<T, Result<TResult>> bind)
            => source.AndThen(bind);

        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TResult>(this Effect<TEnv, T> source, Func<T, Effect<TResult>> bind)
           => source.AndThen(bind);

        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this Effect<TEnv, T> source, Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));
    }
}
