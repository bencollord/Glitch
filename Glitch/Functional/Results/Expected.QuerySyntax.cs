using Glitch.Functional;
using Glitch.Functional.Attributes;
using System.Numerics;

namespace Glitch.Functional.Results
{
    [MonadExtension(typeof(Expected<,>))]
    public static class Result2Extensions
    {
        public static Expected<TResult, E> SelectMany<T, E, TElement, TResult>(this Expected<T, E> source, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

        public static Expected<TResult, E> SelectMany<T, E, TElement, TResult>(this Expected<T, E> source, Func<T, Success<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => (Expected<TResult, E>)bind(s).Select(e => bindMap(s, e)));

        public static Expected<TResult, E> SelectMany<T, E, TElement, TResult>(this Expected<T, E> source, Func<T, Failure<E>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => Expected<TResult, E>.Fail(bind(s).Error));
    }
}