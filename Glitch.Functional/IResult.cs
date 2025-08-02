using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public interface IResult<T, E>
    {
        bool IsOkay { get; }
        bool IsError { get; }

        IResult<TResult, E> Map<TResult>(Func<T, TResult> map);

        IResult<T, TNewError> MapError<TNewError>(Func<E, TNewError> map);

        IResult<T, E> Guard(Func<T, bool> predicate, Func<T, E> error);

        TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> error);

        // TODO Should these be extension methods?
        virtual TResult Match<TResult>(Func<T, TResult> okay, TResult error) => Match(okay, _ => error);

        virtual IResult<TResult, E> Zip<TOther, TResult>(IResult<TOther, E> other, Func<T, TOther, TResult> zipper)
            => AndThen(x => other.Map(zipper.Curry()(x)));

        virtual T IfFail(T fallback) => Match(Identity, _ => fallback);
        virtual T IfFail(Func<E, T> fallback) => Match(Identity, fallback);

        virtual IResult<TResult, E> Cast<TResult>() => Map(v => (TResult)(dynamic)v!);
        virtual IResult<T, TNewError> CastError<TNewError>() => MapError(v => (TNewError)(dynamic)v!);
        virtual IResult<T, E> Do(Action<T> action)
            => Map(v =>
            {
                action(v);
                return v;
            });

        virtual IResult<TResult, E> And<TResult>(IResult<TResult, E> other)
            => Match(okay: _ => other,
                     error: _ => Cast<TResult>());

        virtual IResult<T, E> Or(IResult<T, E> other)
            => Match(okay: _ => this,
                     error: _ => other);

        virtual IResult<TResult, E> AndThen<TResult>(Func<T, IResult<TResult, E>> bind)
            => Match(okay: bind, error: _ => Cast<TResult>());

        virtual IResult<T, TNewError> OrElse<TNewError>(Func<E, IResult<T, TNewError>> bind)
            => Match(okay: _ => CastError<TNewError>(), error: bind);
    }

    public static class IResultExtensions
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