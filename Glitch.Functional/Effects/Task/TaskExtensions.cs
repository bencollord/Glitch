
namespace Glitch.Functional.Effects;

public static partial class TaskExtensions
{
    extension<T>(Task<T> task)
    {
        public async Task<TResult> Select<TResult>(Func<T, TResult> mapper)
        {
            var res = await task.ConfigureAwait(false);
            return mapper(res);
        }

        public Task<TResult> Apply<TResult>(Task<Func<T, TResult>> function)
            => task.AndThen(v => function.Select(fn => fn(v)));

        public async Task<TResult> AndThen<TResult>(Func<T, Task<TResult>> bind)
        {
            var res = await task.ConfigureAwait(false);
            var bnd = await bind(res).ConfigureAwait(false);

            return bnd;
        }

        public async Task<TResult> AndThen<TElement, TResult>(Func<T, Task<TElement>> bind, Func<T, TElement, TResult> project)
        {
            var res = await task.ConfigureAwait(false);
            var bnd = await bind(res).ConfigureAwait(false);

            return project(res, bnd);
        }

        public async Task<T> Where(Func<T, bool> predicate)
        {
            var res = await task.ConfigureAwait(false);

            if (!predicate(res))
            {
                throw new TaskCanceledException();
            }

            return res;
        }

        public async Task<T> Do(Action<T> action)
        {
            var res = await task.ConfigureAwait(false);
            action(res);
            return res;
        }
    }

    extension<T>(Task<T> source)
    {
        public Task<TResult> SelectMany<TResult>(Func<T, Task<TResult>> bind)
        => source.AndThen(bind);

        public Task<TResult> SelectMany<TElement, TResult>(Func<T, Task<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));
    }
}
