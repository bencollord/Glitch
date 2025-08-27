using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    using static Option;

    public static partial class Result
    {
        public static Option<Result<T>> Invert<T>(this Result<Option<T>> result)
            => result.Match(
                okay: (Option<T> opt) => opt.Map(Okay),
                error: (Error err) => Some(Fail<T>(err)));

        public static Result<Option<TResult>> Map<T, TResult>(this Result<Option<T>> result, Func<T, TResult> map)
            => result.Select(opt => opt.Map(map));

        public static Result<Option<T>> Filter<T>(this Result<Option<T>> result, Func<T, bool> predicate)
            => result.Select(opt => opt.Filter(predicate));

        public static Result<Option<TResult>> AndThen<T, TResult>(this Result<Option<T>> result, Func<T, Option<TResult>> bind)
           => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>> AndThen<T, TElement, TResult>(this Result<Option<T>> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => Okay<Option<TElement>>(bind(v)).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>>(Option<TResult>.None)));

        public static Result<Option<TResult>> AndThen<T, TResult>(this Result<Option<T>> result, Func<T, Result<TResult>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>> AndThen<T, TElement, TResult>(this Result<Option<T>> result, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Select(Some).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>>(Option<TResult>.None)));

        public static Result<Option<TResult>> AndThen<T, TResult>(this Result<Option<T>> result, Func<T, Result<Option<TResult>>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Result<Option<TResult>> AndThen<T, TElement, TResult>(this Result<Option<T>> result, Func<T, Result<Option<TElement>>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>>(Option<TResult>.None)));

        // Query Syntax
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>> Select<T, TResult>(this Result<Option<T>> result, Func<T, TResult> map)
            => result.Map(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>> SelectMany<T, TElement, TResult>(this Result<Option<T>> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>> SelectMany<T, TElement, TResult>(this Result<Option<T>> result, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<TResult>> SelectMany<T, TElement, TResult>(this Result<Option<T>> result, Func<T, Result<Option<TElement>>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<Option<T>> Where<T>(this Result<Option<T>> result, Func<T, bool> predicate)
            => result.Filter(predicate);
    }
}
