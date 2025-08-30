using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    using static Expected;

    public static partial class Option
    {
        public static Expected<Option<T>, E> Invert<T, E>(this Option<Expected<T, E>> option)
            => option.Match(
                some: res => res.Map(Some),
                none: () => Okay<Option<T>, E>(None));

        public static Option<Expected<TResult, E>> Map<T, E, TResult>(this Option<Expected<T, E>> option, Func<T, TResult> map)
            => option.Select(opt => opt.Map(map));

        public static Option<Expected<T, E>> Filter<T, E>(this Option<Expected<T, E>> option, Func<T, bool> predicate)
            => option.AndThen(res => res.IsErrorOr(predicate) ? Some(res) : None);

        public static Option<Expected<TResult, E>> AndThen<T, E, TResult>(this Option<Expected<T, E>> option, Func<T, Option<TResult>> bind)
           => option.AndThen(bind, (_, r) => r);

        public static Option<Expected<TResult, E>> AndThen<T, E, TElement, TResult>(this Option<Expected<T, E>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(res => res.Match(okay: v => bind(v).Select(Okay<TElement, E>).Map(project.Curry()(v)),
                                               error: err =>  Some(Fail<TResult, E>(err))));

        public static Option<Expected<TResult, E>> AndThen<T, E, TResult>(this Option<Expected<T, E>> option, Func<T, Expected<TResult, E>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Expected<TResult, E>> AndThen<T, E, TElement, TResult>(this Option<Expected<T, E>> option, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(res => res.Match(okay: v => Some(bind(v)).Map(project.Curry()(v)),
                                               error: e => Some(Fail<TResult, E>(e))));

        public static Option<Expected<TResult, E>> AndThen<T, E, TResult>(this Option<Expected<T, E>> option, Func<T, Option<Expected<TResult, E>>> bind)
            => option.AndThen(bind, (_, r) => r);

        public static Option<Expected<TResult, E>> AndThen<T, E, TElement, TResult>(this Option<Expected<T, E>> option, Func<T, Option<Expected<TElement, E>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(res => res.Match(okay: v => bind(v).Map(project.Curry()(v)),
                                               error: e => Some(Fail<TResult, E>(e))));

        // Query Syntax
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<TResult, E>> Select<T, E, TResult>(this Option<Expected<T, E>> option, Func<T, TResult> map)
            => option.Map(map);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<TResult, E>> SelectMany<T, E, TElement, TResult>(this Option<Expected<T, E>> option, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<TResult, E>> SelectMany<T, E, TElement, TResult>(this Option<Expected<T, E>> option, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<TResult, E>> SelectMany<T, E, TElement, TResult>(this Option<Expected<T, E>> option, Func<T, Option<Expected<TElement, E>>> bind, Func<T, TElement, TResult> project)
            => option.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<Expected<T, E>> Where<T, E>(this Option<Expected<T, E>> option, Func<T, bool> predicate)
            => option.Filter(predicate);
    }
}
