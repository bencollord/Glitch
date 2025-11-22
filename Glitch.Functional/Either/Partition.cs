using Glitch.Functional;
using Glitch.Functional.Results;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    using static Errors;

    public static partial class Either
    {
        public static IEnumerable<T> Successes<T, E>(this IEnumerable<IEither<T, E>> results)
            => results.Where(r => r.IsOkay).Select(r => r.Unwrap());
        public static IEnumerable<E> Errors<T, E>(this IEnumerable<IEither<T, E>> results)
            => results.Where(r => r.IsError).Select(r => r.UnwrapError());
        public static (IEnumerable<T> Successes, IEnumerable<E> Errors) Partition<T, E>(this IEnumerable<IEither<T, E>> results)
            => (results.Successes(), results.Errors());
    }
}