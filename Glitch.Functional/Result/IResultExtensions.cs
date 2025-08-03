using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static class ResultExtensions
    {
        // State querying
        // ===============================================================================
        public static bool IsOkayAnd<T, E>(this IResult<T, E> result, Func<T, bool> predicate) => result.Match(predicate, false);

        public static bool IsErrorOr<T, E>(this IResult<T, E> result, Func<T, bool> predicate) => result.Match(predicate, true);

        // Natural transformations
        // ===============================================================================
        public static IResult<T, E> AsResult<T, E>(this IResult<T, E> result) => result;

        public static Result<T, E> ToResult<T, E>(this IResult<T, E> result) => result.Match(Result.Okay<T, E>, Result.Fail<T, E>);

        public static Option<T> OkayOrNone<T, E>(this IResult<T, E> result) => result.Match(Option.Some, _ => Option.None);

        public static Option<E> ErrorOrNone<T, E>(this IResult<T, E> result) => result.Match(_ => Option.None, Option.Some);

        // Linq
        // ===============================================================================
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResult<TResult, TError> Select<TSuccess, TError, TResult>(this IResult<TSuccess, TError> result, Func<TSuccess, TResult> map)
            => result.Map(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResult<TResult, TError> SelectMany<TSuccess, TError, TResult>(this IResult<TSuccess, TError> result, Func<TSuccess, IResult<TResult, TError>> bind)
            => result.AndThen(bind);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResult<TResult, TError> SelectMany<TSuccess, TElement, TError, TResult>(this IResult<TSuccess, TError> result, Func<TSuccess, IResult<TElement, TError>> bind, Func<TSuccess, TElement, TResult> projection)
            => result.AndThen(v => bind(v).Map(x => projection(v, x)));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResult<TSuccess, TError> Where<TSuccess, TError>(this IResult<TSuccess, TError> result, Func<TSuccess, bool> predicate)
            where TError : ICanBeEmpty<TError>
        {
            return result.Guard(predicate, _ => TError.Empty);
        }
    }
}