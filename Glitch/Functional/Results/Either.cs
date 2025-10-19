using Glitch.Functional.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static IEither<T, E> Okay<T, E>(T value) => new Okay<T, E>(value);
        public static IEither<T, E> Fail<T, E>(E error) => new Fail<T, E>(error);

        public static IEither<T, E> AsEither<T, E>(this IEither<T, E> source) => source;

        public static bool IsOkay<T, E>(this IEither<T, E> source, [NotNullWhen(true)] out T? value) => source.TryUnwrap(out value);

        public static bool IsOkayAnd<T, E>(this IEither<T, E> source, Func<T, bool> predicate) => source.Match(predicate, false);

        public static bool IsError<T, E>(this IEither<T, E> source, [NotNullWhen(true)] out E? error) => source.TryUnwrapError(out error);
        
        public static bool IsErrorOr<T, E>(this IEither<T, E> source, Func<T, bool> predicate) => source.Match(predicate, true);
        public static IEither<T, E> Flatten<T, E>(this IEither<IEither<T, E>, E> nested)
            => nested.AndThen(n => n);
    }
}