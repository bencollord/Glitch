using Glitch.Functional.Attributes;

namespace Glitch.Functional.Results
{
    [MonadExtension(typeof(Result<,>))]
    public static class Result2Extensions
    {
        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(s => bind(s).Select(e => project(s, e)));

        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Success<TElement>> bind, Func<T, TElement, TResult> project)
            => source.Select(s => project(s, bind(s).Value));

        public static Result<TResult, E> SelectMany<T, E, TElement, TResult>(this Result<T, E> source, Func<T, Failure<E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(s => Result.Fail<TResult, E>(bind(s).Error));
    }
}