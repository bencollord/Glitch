using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

public static class NaturalTransformations
{
    extension<T, E>(Result<T, E> source)
    {
        public Option<T> OkayOrNone() => source.Match(Option.Some, _ => Option.None);

        public Option<E> ErrorOrNone() => source.Match(_ => Option.None, Option.Some);
    }

    extension<T>(Expected<T> source)
    {
        public Option<T> OkayOrNone() => source.Match(Option.Some, _ => Option.None);

        public Option<Error> ErrorOrNone() => source.Match(_ => Option.None, Option.Some);
    }
}
