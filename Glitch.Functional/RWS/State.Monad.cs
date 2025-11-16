using Glitch.Functional.Core;

namespace Glitch.Functional
{
    public static partial class State
    {
        public static IStateful<S, TResult> Select<S, T, TResult>(this IStateful<S, T> source, Func<T, TResult> map)
            => new MapState<S, T, TResult>(source, map);

        public static IStateful<S, TResult> AndThen<S, T, TResult>(this IStateful<S, T> source, Func<T, IStateful<S, TResult>> bind)
            => new BindState<S, T, TResult>(source, bind);

        public static IStateful<S, TResult> AndThen<S, T, TElement, TResult>(this IStateful<S, T> source, Func<T, IStateful<S, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(x => bind(x).Select(project.Curry(x)));

        public static IStateful<S, TResult> SelectMany<S, T, TElement, TResult>(this IStateful<S, T> source, Func<T, IStateful<S, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        private class MapState<S, T, TResult> : IStateful<S, TResult>
        {
            private readonly IStateful<S, T> source;
            private readonly Func<T, TResult> map;

            internal MapState(IStateful<S, T> source, Func<T, TResult> map)
            {
                this.source = source;
                this.map = map;
            }

            public StateResult<S, TResult> Run(S state)
            {
                var (s, v) = source.Run(state);

                return new(s, map(v));
            }
        }

        private class BindState<S, T, TResult> : IStateful<S, TResult>
        {
            private readonly IStateful<S, T> source;
            private readonly Func<T, IStateful<S, TResult>> bind;

            internal BindState(IStateful<S, T> source, Func<T, IStateful<S, TResult>> bind)
            {
                this.source = source;
                this.bind = bind;
            }

            public StateResult<S, TResult> Run(S state)
            {
                var (s, v) = source.Run(state);

                return bind(v).Run(s);
            }
        }
    }
}
