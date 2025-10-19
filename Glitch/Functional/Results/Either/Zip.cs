using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static IEither<TResult, E> Zip<T, E, TOther, TResult>(this IEither<T, E> source, IEither<TOther, E> other, Func<T, TOther, TResult> zipper)
            => source.AndThen(x => other.Select(y => zipper(x, y)));

        public static IEither<(T First, TOther Second), E> Zip<T, E, TOther>(this IEither<T, E> source, IEither<TOther, E> other)
            => source.Zip(other, (x, y) => (x, y));
    }
}