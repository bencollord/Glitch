using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static Option<T> OkayOrNone<T, E>(this IEither<T, E> source)
            => source is Option<T> opt
             ? opt // Special case since Option<T> implements IResult<T, Unit>
             : source.Match(Option<T>.Some, _ => Option<T>.None);

        public static Option<E> ErrorOrNone<T, E>(this IEither<T, E> source) => source.Match(_ => Option<E>.None, Some);
    }
}