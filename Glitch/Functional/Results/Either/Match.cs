using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static Unit Match<T, E>(this IEither<T, E> source, Action<T> okay, Action<E> error) => source.Match(okay.Return(Unit.Value), error.Return(Unit.Value));

        public static TResult Match<T, E, TResult>(this IEither<T, E> source, Func<T, TResult> okay, Func<TResult> error)
            => source.Match(okay, _ => error());

        public static TResult Match<T, E, TResult>(this IEither<T, E> source, Func<T, TResult> okay, TResult error)
            => source.IsOkay ? okay(source.Unwrap()) : error; // Avoid unnecessary delegate

        public static T Match<T, E>(this IEither<bool, E> result, Func<Unit, T> @true, Func<Unit, T> @false, Func<E, T> error)
            => result.Match(flag => flag ? @true(default) : @false(default), error);
        public static T Match<T, E>(this IEither<bool, E> result, Func<T> @true, Func<T> @false, Func<E, T> error)
            => result.Match(flag => flag ? @true() : @false(), error);
        public static Unit Match<E>(this IEither<bool, E> result, Action @true, Action @false, Action<E> error)
            => result.Match(flag => flag ? @true.Return()() : @false.Return()(), error.Return());
        public static Unit Match<E>(this IEither<bool, E> result, Action<Unit> @true, Action<Unit> @false, Action<E> error)
            => result.Match(flag => flag ? @true.Return()(default) : @false.Return()(default), error.Return());
    }
}