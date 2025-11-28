namespace Glitch.Functional.Effects;

public static partial class State
{
    extension<S, T>(State<S, T> source)
    {
        public State<S, TResult> Select<TResult>(Func<T, TResult> map)
            => s =>
            {
                var (next, value) = source(s);

                return new(next, map(value));
            };

        public State<S, TResult> Apply<TResult>(State<S, Func<T, TResult>> apply)
            => apply.AndThen(fn => source.Select(fn));

        public State<S, TResult> Then<TResult>(State<S, TResult> next)
            => source.AndThen(_ => next);

        public State<S, TResult> AndThen<TResult>(Func<T, State<S, TResult>> bind)
            => s =>
            {
                var (next, value) = source(s);
                
                return bind(value)(next);
            };

        public State<S, TResult> AndThen<TElement, TResult>(Func<T, State<S, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(x => bind(x).Select(project.Curry(x)));

        public State<S, TResult> SelectMany<TElement, TResult>(Func<T, State<S, TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);
    }
}
