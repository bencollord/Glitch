using Glitch.Functional.Results;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        public static Option<Expected<T, E>> Invert<T, E>(this Expected<Option<T>, E> result)
            => result.Match(
                okay: opt => opt.Select(Okay<T, E>),
                error: err => Option.Some(Fail<T, E>(err)));

        public static Expected<Option<TResult>, E> Select<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, TResult> map)
            => result.Select(opt => opt.Select(map));

        public static Expected<Option<T>, E> Where<T, E>(this Expected<Option<T>, E> result, Func<T, bool> predicate)
            => result.Select(opt => opt.Where(predicate));

        public static Expected<Option<TResult>, E> AndThen<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, Option<TResult>> bind)
           => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen((Func<Option<T>, Expected<Option<TResult>, E>>)(opt => opt.Match(some: (Func<T, Expected<Option<TResult>, E>>)(v => Expected.Select<TElement, E, TResult>(Okay<Option<TElement>, E>(bind(v)), project.Curry()(v))),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None))));

        public static Expected<Option<TResult>, E> AndThen<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<TResult, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen((Func<Option<T>, Expected<Option<TResult>, E>>)(opt => opt.Match(some: (Func<T, Expected<Option<TResult>, E>>)(v => Expected.Select<TElement, E, TResult>(bind(v).Select(Option.Some), project.Curry()(v))),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None))));

        public static Expected<Option<TResult>, E> AndThen<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<Option<TResult>, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<Option<TElement>, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen((Func<Option<T>, Expected<Option<TResult>, E>>)(opt => opt.Match(some: (Func<T, Expected<Option<TResult>, E>>)(v => Expected.Select<TElement, E, TResult>(bind(v), project.Curry()(v))),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None))));

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>, E> SelectMany<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>, E> SelectMany<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>, E> SelectMany<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<Option<TElement>, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(bind, project);
    }
}
