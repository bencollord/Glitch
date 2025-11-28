namespace Glitch.Functional.Effects;

public static partial class ValueTaskExtensions
{
    extension<T>(ValueTask<T> task)
    {
        public async ValueTask<TResult> Select<TResult>(Func<T, TResult> mapper)
        {
            var res = await task.ConfigureAwait(false);
            return mapper(res);
        }

        public ValueTask<TResult> Apply<TResult>(ValueTask<Func<T, TResult>> function)
            => task.AndThen(v => function.Select(fn => fn(v)));

        public async ValueTask<TResult> AndThen<TResult>(Func<T, ValueTask<TResult>> bind)
        {
            var res = await task.ConfigureAwait(false);
            var bnd = await bind(res).ConfigureAwait(false);

            return bnd;
        }

        public async ValueTask<TResult> AndThen<TElement, TResult>(Func<T, ValueTask<TElement>> bind, Func<T, TElement, TResult> project)
        {
            var res = await task.ConfigureAwait(false);
            var bnd = await bind(res).ConfigureAwait(false);

            return project(res, bnd);
        }

        public async ValueTask<T> Where(Func<T, bool> predicate)
        {
            var res = await task.ConfigureAwait(false);

            if (!predicate(res))
            {
                throw new TaskCanceledException();
            }

            return res;
        }

        public async ValueTask<T> Do(Action<T> action)
        {
            var res = await task.ConfigureAwait(false);
            action(res);
            return res;
        }

        public ValueTask<TResult> SelectMany<TResult>(Func<T, ValueTask<TResult>> bind)
            => task.AndThen(bind);

        public ValueTask<TResult> SelectMany<TElement, TResult>(Func<T, ValueTask<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => task.AndThen(s => bind(s).Select(e => bindMap(s, e)));
    }
}
