using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static class IOExtensions
    {
        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, IO<TElement>> bind, Func<T, TElement, TResult> project) 
            => source.AndThen(bind, project);

        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        public static IO<TResult> SelectMany<T, E, TElement, TResult>(this IO<T> source, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        public static Effect<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        public static Effect<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this IO<T> source, Func<T, Effect<TEnv, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);
    }
}