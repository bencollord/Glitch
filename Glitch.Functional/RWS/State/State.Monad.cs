namespace Glitch.Functional;

public static partial class State
{
    extension<S, T>(IStateful<S, T> source)
    {
        public IStateful<S, TResult> Select<TResult>(Func<T, TResult> map)
            => new MapState<S, T, TResult>(source, map);

        public IStateful<S, TResult> Apply<TResult>(IStateful<S, Func<T, TResult>> apply)
            => apply.AndThen(fn => source.Select(fn));

        public IStateful<S, TResult> Then<TResult>(IStateful<S, TResult> next)
            => source.AndThen(_ => next);

        public IStateful<S, TResult> AndThen<TResult>(Func<T, IStateful<S, TResult>> bind)
            => new BindState<S, T, TResult>(source, bind);

        public IStateful<S, TResult> AndThen<TElement, TResult>(Func<T, IStateful<S, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(x => bind(x).Select(project.Curry(x)));

        public IStateful<S, TResult> SelectMany<TElement, TResult>(Func<T, IStateful<S, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);
    }

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
