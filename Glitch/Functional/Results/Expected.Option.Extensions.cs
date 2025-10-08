using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    using static Option;

    public static partial class Expected
    {
        public static Option<Expected<T>> Invert<T>(this Expected<Option<T>> result)
            => result.Match(
                okay: (Option<T> opt) => opt.Select(Okay),
                error: (Error err) => Some(Fail<T>(err)));

        public static Expected<Option<TResult>> Map<T, TResult>(this Expected<Option<T>> result, Func<T, TResult> map)
            => result.Select(opt => opt.Select(map));

        public static Expected<Option<T>> Filter<T>(this Expected<Option<T>> result, Func<T, bool> predicate)
            => result.Select(opt => opt.Where(predicate));

        public static Expected<Option<TResult>> AndThen<T, TResult>(this Expected<Option<T>> result, Func<T, Option<TResult>> bind)
           => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>> AndThen<T, TElement, TResult>(this Expected<Option<T>> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => Okay<Option<TElement>>(bind(v)).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>>(Option<TResult>.None)));

        public static Expected<Option<TResult>> AndThen<T, TResult>(this Expected<Option<T>> result, Func<T, Expected<TResult>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>> AndThen<T, TElement, TResult>(this Expected<Option<T>> result, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Select(Some).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>>(Option<TResult>.None)));

        public static Expected<Option<TResult>> AndThen<T, TResult>(this Expected<Option<T>> result, Func<T, Expected<Option<TResult>>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>> AndThen<T, TElement, TResult>(this Expected<Option<T>> result, Func<T, Expected<Option<TElement>>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>>(Option<TResult>.None)));

        // Query Syntax
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>> Select<T, TResult>(this Expected<Option<T>> result, Func<T, TResult> map)
            => result.Map(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>> SelectMany<T, TElement, TResult>(this Expected<Option<T>> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>> SelectMany<T, TElement, TResult>(this Expected<Option<T>> result, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>> SelectMany<T, TElement, TResult>(this Expected<Option<T>> result, Func<T, Expected<Option<TElement>>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<T>> Where<T>(this Expected<Option<T>> result, Func<T, bool> predicate)
            => result.Filter(predicate);
    }
}
