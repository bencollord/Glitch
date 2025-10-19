using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static IEither<TResult, E> ApplyTo<T, E, TResult>(this IEither<T, E> source, IEither<Func<T, TResult>, E> function)
            => source.AndThen(x => function.Select(fn => fn(x)));

        public static IEither<TResult, E> Apply<T, E, TResult>(this IEither<Func<T, TResult>, E> source, IEither<T, E> value)
            => source.AndThen(fn => value.Select(x => fn(x)));
    }
}