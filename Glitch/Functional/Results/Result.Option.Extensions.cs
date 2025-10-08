using Glitch.Functional.Results;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public static partial class Result
    {
        public static Option<Result<T, E>> Invert<T, E>(this Result<Option<T>, E> result)
            => result.Match(
                okay: opt => opt.Select(Okay<T, E>),
                error: err => Option.Some(Fail<T, E>(err)));

        public static Result<Option<TResult>, E> Select<T, E, TResult>(this Result<Option<T>, E> result, Func<T, TResult> map)
            => result.Select(opt => opt.Select(map));

        public static Result<Option<T>, E> Where<T, E>(this Result<Option<T>, E> result, Func<T, bool> predicate)
            => result.Select(opt => opt.Where(predicate));

        public static Result<Option<TResult>, E> AndThen<T, E, TResult>(this Result<Option<T>, E> result, Func<T, Option<TResult>> bind)
           => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen((Func<Option<T>, Result<Option<TResult>, E>>)(opt => opt.Match(some: (Func<T, Result<Option<TResult>, E>>)(v => Result.Select<TElement, E, TResult>(Okay<Option<TElement>, E>(bind(v)), project.Curry()(v))),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None))));

        public static Result<Option<TResult>, E> AndThen<T, E, TResult>(this Result<Option<T>, E> result, Func<T, Result<TResult, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen((Func<Option<T>, Result<Option<TResult>, E>>)(opt => opt.Match(some: (Func<T, Result<Option<TResult>, E>>)(v => Result.Select<TElement, E, TResult>(bind(v).Select(Option.Some), project.Curry()(v))),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None))));

        public static Result<Option<TResult>, E> AndThen<T, E, TResult>(this Result<Option<T>, E> result, Func<T, Result<Option<TResult>, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Result<Option<TElement>, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen((Func<Option<T>, Result<Option<TResult>, E>>)(opt => opt.Match(some: (Func<T, Result<Option<TResult>, E>>)(v => Result.Select<TElement, E, TResult>(bind(v), project.Curry()(v))),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None))));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>, E> SelectMany<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>, E> SelectMany<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>, E> SelectMany<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Result<Option<TElement>, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);
    }
}
