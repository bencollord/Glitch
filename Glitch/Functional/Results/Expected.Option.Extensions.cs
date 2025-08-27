using Glitch.Functional.Results;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        public static Option<Expected<T, E>> Invert<T, E>(this Expected<Option<T>, E> result)
            => result.Match(
                okay: opt => opt.Map(Okay<T, E>),
                error: err => Option.Some(Fail<T, E>(err)));

        public static Expected<Option<TResult>, E> Map<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, TResult> map)
            => result.Map(opt => opt.Map(map));

        public static Expected<Option<T>, E> Filter<T, E>(this Expected<Option<T>, E> result, Func<T, bool> predicate)
            => result.Map(opt => opt.Filter(predicate));

        public static Expected<Option<TResult>, E> AndThen<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, Option<TResult>> bind)
           => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => Okay<Option<TElement>, E>(bind(v)).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None)));

        public static Expected<Option<TResult>, E> AndThen<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<TResult, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<TElement, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Map(Option.Some).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None)));

        public static Expected<Option<TResult>, E> AndThen<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<Option<TResult>, E>> bind)
            => result.AndThen(bind, (_, r) => r);

        public static Expected<Option<TResult>, E> AndThen<T, E, TElement, TResult>(this Expected<Option<T>, E> result, Func<T, Expected<Option<TElement>, E>> bind, Func<T, TElement, TResult> project)
            => result.AndThen(opt => opt.Match(some: v => bind(v).Map(project.Curry()(v)),
                                               none: Okay<Option<TResult>, E>(Option<TResult>.None)));

        // Query Syntax
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<TResult>, E> Select<T, E, TResult>(this Expected<Option<T>, E> result, Func<T, TResult> map)
            => result.Map(map);

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

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<Option<T>, E> Where<T, E>(this Expected<Option<T>, E> result, Func<T, bool> predicate)
            => result.Filter(predicate);
    }
}
