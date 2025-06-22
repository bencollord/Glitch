namespace Glitch.Functional
{
    public static partial class TaskExtensions
    {
        public static async Task<TResult> Map<T, TResult>(this Task<T> task, Func<T, TResult> mapper)
        {
            var res = await task.ConfigureAwait(false);
            return mapper(res);
        }

        public static Task<TResult> Apply<T, TResult>(this Task<T> task, Task<Func<T, TResult>> function)
            => task.AndThen(v => function.Map(fn => fn(v)));

        public static async Task<TResult> AndThen<T, TResult>(this Task<T> task, Func<T, Task<TResult>> bind)
        {
            var res = await task.ConfigureAwait(false);
            var bnd = await bind(res).ConfigureAwait(false);

            return bnd;
        }

        public static async Task<TResult> AndThen<T, TElement, TResult>(this Task<T> task, Func<T, Task<TElement>> bind, Func<T, TElement, TResult> project)
        {
            var res = await task.ConfigureAwait(false);
            var bnd = await bind(res).ConfigureAwait(false);

            return project(res, bnd);
        }

        public static async Task<T> Filter<T>(this Task<T> task, Func<T, bool> predicate)
        {
            var res = await task.ConfigureAwait(false);

            if (!predicate(res))
            {
                throw new TaskCanceledException();
            }

            return res;
        }

        public static Task<TResult> Select<T, TResult>(this Task<T> source, Func<T, TResult> mapper)
            => source.Map(mapper);

        public static Task<TResult> SelectMany<T, TResult>(this Task<T> source, Func<T, Task<TResult>> bind)
            => source.AndThen(bind);

        public static Task<TResult> SelectMany<T, TElement, TResult>(this Task<T> source, Func<T, Task<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Task<T> Where<T>(this Task<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);

        public static async Task<T> Do<T>(this Task<T> task, Action<T> action)
        {
            var res = await task.ConfigureAwait(false);
            action(res);
            return res;
        }
    }
}
