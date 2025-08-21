using System.Numerics;

namespace Glitch.Functional
{
    public static class Result2Extensions
    {
        public static Result<TResult, E> Select<T, E, TResult>(this Result<T, E> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Success<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => (Result<TResult, E>)bind(s).Map(e => bindMap(s, e)));

        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Failure<E>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => Result<TResult, E>.Fail(bind(s).Error));
    }
}