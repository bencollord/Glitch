using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static partial class Either
    {
        public static IEnumerable<T> Iterate<T, E>(this IEither<T, E> source) => source.Match(x => [x], _ => Enumerable.Empty<T>());
    }
}