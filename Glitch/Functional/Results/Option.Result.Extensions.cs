using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    using static Result;

    public static partial class Option
    {
        public static Result<Option<T>> Invert<T>(this Option<Result<T>> option)
            => option.Match(
                some: res => res.Select(Some),
                none: () => Okay<Option<T>>(None));

        public static Option<Result<TResult>> Map<T, TResult>(this Option<Result<T>> option, Func<T, TResult> map)
            => option.Map(opt => opt.Select(map));

        public static Option<Result<T>> Filter<T>(this Option<Result<T>> option, Func<T, bool> predicate)
            => option.AndThen(res => res.IsErrorOr(predicate) ? Some(res) : None);

        public static Option<Result<TResult>> AndThen<T, TResult>(this Option<Result<T>> option, Func<T, Option<TResult>> bind)
           => option.AndThen(bind, (_, r) => r);

        public static Option<Result<TResult>> AndThen<T, TElement, TResult>(this Option<Result<T>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(res => res.Match(okay: v => bind(v).Map(Okay<TElement>).Map(project.Curry()(v)),
                                               error: err =>  Some(Fail<TResult>(err))));

        public static Option<Result<TResult>> AndThen<T, TResult>(this Option<Result<T>> option, Func<T, Result<TResult>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Result<TResult>> AndThen<T, TElement, TResult>(this Option<Result<T>> option, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(res => res.Match(okay: v => Some(bind(v)).Map(project.Curry()(v)),
                                               error: e => Some(Fail<TResult>(e))));

        public static Option<Result<TResult>> AndThen<T, TResult>(this Option<Result<T>> option, Func<T, Option<Result<TResult>>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Result<TResult>> AndThen<T, TElement, TResult>(this Option<Result<T>> option, Func<T, Option<Result<TElement>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(res => res.Match(okay: v => bind(v).Map(project.Curry()(v)),
                                               error: e => Some(Fail<TResult>(e))));

        // Query Syntax
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<TResult>> Select<T, TResult>(this Option<Result<T>> option, Func<T, TResult> map)
            => option.Map(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<TResult>> SelectMany<T, TElement, TResult>(this Option<Result<T>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<TResult>> SelectMany<T, TElement, TResult>(this Option<Result<T>> option, Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<TResult>> SelectMany<T, TElement, TResult>(this Option<Result<T>> option, Func<T, Option<Result<TElement>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<T>> Where<T>(this Option<Result<T>> option, Func<T, bool> predicate)
            => option.Filter(predicate);
    }
}
