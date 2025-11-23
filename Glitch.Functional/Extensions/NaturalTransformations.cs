using Glitch.Functional.Core;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions
{
    public static class NaturalTransformations
    {
        public static Option<T> OkayOrNone<T, E>(this Result<T, E> source) => source.Match(Option.Some, _ => Option.None);
        public static Option<T> OkayOrNone<T>(this Expected<T> source) => source.Match(Option.Some, _ => Option.None);
    }
}
