using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    using static Result;

    public static partial class Option
    {
        public static Result<Option<T>, E> Invert<T, E>(this Option<Result<T, E>> option)
            => option.Match(
                some: res => res.Select(Some),
                none: () => Okay<Option<T>, E>(None));

        public static Option<Result<TResult, E>> Select<T, E, TResult>(this Option<Result<T, E>> option, Func<T, TResult> map)
            => option.Select(opt => opt.Select(map));

        public static Option<Result<T, E>> Where<T, E>(this Option<Result<T, E>> option, Func<T, bool> predicate)
            => option.AndThen(res => res.IsErrorOr(predicate) ? Some(res) : None);

        public static Option<Result<TResult, E>> AndThen<T, E, TResult>(this Option<Result<T, E>> option, Func<T, Option<TResult>> bind)
           => option.AndThen(bind, (_, r) => r);

        public static Option<Result<TResult, E>> AndThen<T, E, TElement, TResult>(this Option<Result<T, E>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen((Func<Result<T, E>, Option<Result<TResult, E>>>)(res => res.Match(okay: (Func<T, Option<Result<TResult, E>>>)(v => Option.Select<TElement, E, TResult>(bind(v).Select(Okay<TElement, E>), project.Curry()(v))),
                                               error: err => Some(Fail<TResult, E>(err)))));

        public static Option<Result<TResult, E>> AndThen<T, E, TResult>(this Option<Result<T, E>> option, Func<T, Result<TResult, E>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Result<TResult, E>> AndThen<T, E, TElement, TResult>(this Option<Result<T, E>> option, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => option.AndThen((Func<Result<T, E>, Option<Result<TResult, E>>>)(res => res.Match(okay: (Func<T, Option<Result<TResult, E>>>)(v => Option.Select<TElement, E, TResult>(Some(bind(v)), project.Curry()(v))),
                                               error: e => Some(Fail<TResult, E>(e)))));

        public static Option<Result<TResult, E>> AndThen<T, E, TResult>(this Option<Result<T, E>> option, Func<T, Option<Result<TResult, E>>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Result<TResult, E>> AndThen<T, E, TElement, TResult>(this Option<Result<T, E>> option, Func<T, Option<Result<TElement, E>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen((Func<Result<T, E>, Option<Result<TResult, E>>>)(res => res.Match(okay: (Func<T, Option<Result<TResult, E>>>)(v => Option.Select<TElement, E, TResult>(bind(v), project.Curry()(v))),
                                               error: e => Some(Fail<TResult, E>(e)))));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<TResult, E>> SelectMany<T, E, TElement, TResult>(this Option<Result<T, E>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<TResult, E>> SelectMany<T, E, TElement, TResult>(this Option<Result<T, E>> option, Func<T, Result<TElement, E>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Result<TResult, E>> SelectMany<T, E, TElement, TResult>(this Option<Result<T, E>> option, Func<T, Option<Result<TElement, E>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);
    }
}
