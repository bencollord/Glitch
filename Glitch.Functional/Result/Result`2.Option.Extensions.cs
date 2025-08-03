using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static partial class Result
    {
        public static Option<Result<T, E>> Invert<T, E>(this Result<Option<T>, E> result)
            => result.Match(
                okay: opt => opt.Map(Okay<T, E>),
                error: err => Some(Fail<T, E>(err)));

        public static Result<Option<TResult>, E> Map<T, E, TResult>(this Result<Option<T>, E> result, Func<T, TResult> map)
            => result.Map(opt => opt.Map(map));

        public static Result<Option<T>, E> Filter<T, E>(this Result<Option<T>, E> result, Func<T, bool> predicate)
            => result.Map(opt => opt.Filter(predicate));

        public static Result<Option<TResult>, E> AndThen<T, E, TResult>(this Result<Option<T>, E> result, Func<T, Option<TResult>> bind)
           => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => Okay<Option<TElement>, E>(bind(v)).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None)));

        public static Result<Option<TResult>, E> AndThen<T, E, TResult>(this Result<Option<T>, E> result, Func<T, Result<TResult, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Map(Some).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None)));

        public static Result<Option<TResult>, E> AndThen<T, E, TResult>(this Result<Option<T>, E> result, Func<T, Result<Option<TResult>, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Result<Option<T>, E> result, Func<T, Result<Option<TElement>, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None)));

        // Query Syntax
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>, E> Select<T, E, TResult>(this Result<Option<T>, E> result, Func<T, TResult> map)
            => result.Map(map);

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

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<T>, E> Where<T, E>(this Result<Option<T>, E> result, Func<T, bool> predicate)
            => result.Filter(predicate);
    }
}
