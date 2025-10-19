using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static IEither<T, E> Where<T, E>(this IEither<T, E> source, Func<T, bool> predicate)
            where E : ICanBeEmpty<E>
            => source.Guard(predicate, E.Empty);

        public static IEither<T, Unit> Where<T>(this IEither<T, Unit> source, Func<T, bool> predicate)
            => source.Guard(predicate, Unit.Value);

        public static IEither<T, E> Guard<T, E>(this IEither<T, E> source, bool condition, E error)
            => condition ? source : new Fail<T, E>(error);

        public static IEither<T, E> Guard<T, E>(this IEither<T, E> source, bool condition, Func<T, E> error)
            => source.AndThen(x => condition ? source : new Fail<T, E>(error(x)));

        public static IEither<T, E> Guard<T, E>(this IEither<T, E> source, Func<T, bool> predicate, E error)
            => source.Guard(source.IsOkayAnd(predicate), error);

        public static IEither<T, E> Guard<T, E>(this IEither<T, E> source, Func<T, bool> predicate, Func<T, E> error)
            => source.Guard(source.IsOkayAnd(predicate), error);
    }
}