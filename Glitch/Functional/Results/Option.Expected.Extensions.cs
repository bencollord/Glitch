using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    using static Expected;

    public static partial class Option
    {
        public static Expected<Option<T>> Invert<T>(this Option<Expected<T>> option)
            => option.Match(
                some: res => res.Select(Some),
                none: () => Okay<Option<T>>(None));

        public static Option<Expected<TResult>> Select<T, TResult>(this Option<Expected<T>> option, Func<T, TResult> map)
            => option.Select(opt => opt.Select(map));

        public static Option<Expected<T>> Where<T>(this Option<Expected<T>> option, Func<T, bool> predicate)
            => option.AndThen(res => res.IsErrorOr(predicate) ? Some(res) : None);

        public static Option<Expected<TResult>> AndThen<T, TResult>(this Option<Expected<T>> option, Func<T, Option<TResult>> bind)
           => option.AndThen(bind, (_, r) => r);

        public static Option<Expected<TResult>> AndThen<T, TElement, TResult>(this Option<Expected<T>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen((Func<Expected<T>, Option<Expected<TResult>>>)(res => res.Match(okay: (Func<T, Option<Expected<TResult>>>)(v => Option.Select<TElement, TResult>(bind(v).Select(Okay), project.Curry()(v))),
                                               error: err => Some(Fail<TResult>(err)))));

        public static Option<Expected<TResult>> AndThen<T, TResult>(this Option<Expected<T>> option, Func<T, Expected<TResult>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Expected<TResult>> AndThen<T, TElement, TResult>(this Option<Expected<T>> option, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen((Func<Expected<T>, Option<Expected<TResult>>>)(res => res.Match(okay: (Func<T, Option<Expected<TResult>>>)(v => Option.Select<TElement, TResult>(Some(bind(v)), project.Curry()(v))),
                                               error: e => Some(Fail<TResult>(e)))));

        public static Option<Expected<TResult>> AndThen<T, TResult>(this Option<Expected<T>> option, Func<T, Option<Expected<TResult>>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Expected<TResult>> AndThen<T, TElement, TResult>(this Option<Expected<T>> option, Func<T, Option<Expected<TElement>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen((Func<Expected<T>, Option<Expected<TResult>>>)(res => res.Match(okay: (Func<T, Option<Expected<TResult>>>)(v => Option.Select<TElement, TResult>(bind(v), project.Curry()(v))),
                                               error: e => Some(Fail<TResult>(e)))));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<TResult>> SelectMany<T, TElement, TResult>(this Option<Expected<T>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<TResult>> SelectMany<T, TElement, TResult>(this Option<Expected<T>> option, Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<TResult>> SelectMany<T, TElement, TResult>(this Option<Expected<T>> option, Func<T, Option<Expected<TElement>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);
    }
}
