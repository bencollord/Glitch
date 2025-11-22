using Glitch.Functional.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    using static Glitch.Functional.Errors;

    public static partial class Either
    {
        public static T Unwrap<T, E>(this IEither<T, E> source) => source.Match(Identity, err => BadUnwrap(err).Throw<T>());

        public static E UnwrapError<T, E>(this IEither<T, E> source) => source.Match(val => BadUnwrapError(val).Throw<E>(), Identity);

        public static bool TryUnwrap<T, E>(this IEither<T, E> source, [NotNullWhen(true)] out T? result)
        {
            if (source.IsOkay)
            {
                result = source.Unwrap()!;
                return true;
            }

            result = default!;
            return false;
        }

        public static bool TryUnwrapError<T, E>(this IEither<T, E> source, [NotNullWhen(true)] out E result)
        {
            if (source.IsError)
            {
                result = source.UnwrapError()!;
                return true;
            }

            result = default!;
            return false;
        }

        public static T UnwrapOr<T, E>(this IEither<T, E> source, T fallback) => source.Match(Identity, fallback);
        public static T UnwrapOrElse<T, E>(this IEither<T, E> source, Func<E, T> fallback) => source.Match(Identity, fallback);
        public static T UnwrapOrElse<T, E>(this IEither<T, E> source, Func<T> fallback) => source.Match(Identity, fallback);
        public static T? UnwrapOrDefault<T, E>(this IEither<T, E> source) => source.UnwrapOr(default);
        public static E UnwrapErrorOr<T, E>(this IEither<T, E> source, E fallback) => source.IsError ? source.UnwrapError() : fallback;
        public static E UnwrapErrorOrElse<T, E>(this IEither<T, E> source, Func<T, E> fallback) => source.Match(fallback, Identity);
        public static E UnwrapErrorOrElse<T, E>(this IEither<T, E> source, Func<E> fallback) => source.Match(_ => fallback(), Identity);
        public static E? UnwrapErrorOrDefault<T, E>(this IEither<T, E> source) => source.UnwrapErrorOr(default);
    }
}