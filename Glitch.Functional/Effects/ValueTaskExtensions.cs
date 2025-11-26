namespace Glitch.Functional.Effects;

public static partial class ValueTaskExtensions
{
    public static async ValueTask<TResult> Select<T, TResult>(this ValueTask<T> task, Func<T, TResult> mapper)
    {
        var res = await task.ConfigureAwait(false);
        return mapper(res);
    }

    public static ValueTask<TResult> Apply<T, TResult>(this ValueTask<T> task, ValueTask<Func<T, TResult>> function)
        => task.AndThen(v => function.Select(fn => fn(v)));

    public static async ValueTask<TResult> AndThen<T, TResult>(this ValueTask<T> task, Func<T, ValueTask<TResult>> bind)
    {
        var res = await task.ConfigureAwait(false);
        var bnd = await bind(res).ConfigureAwait(false);

        return bnd;
    }

    public static async ValueTask<TResult> AndThen<T, TElement, TResult>(this ValueTask<T> task, Func<T, ValueTask<TElement>> bind, Func<T, TElement, TResult> project)
    {
        var res = await task.ConfigureAwait(false);
        var bnd = await bind(res).ConfigureAwait(false);

        return project(res, bnd);
    }

    public static async ValueTask<T> Where<T>(this ValueTask<T> task, Func<T, bool> predicate)
    {
        var res = await task.ConfigureAwait(false);

        if (!predicate(res))
        {
            throw new TaskCanceledException();
        }

        return res;
    }

    public static ValueTask<TResult> SelectMany<T, TResult>(this ValueTask<T> source, Func<T, ValueTask<TResult>> bind)
        => source.AndThen(bind);

    public static ValueTask<TResult> SelectMany<T, TElement, TResult>(this ValueTask<T> source, Func<T, ValueTask<TElement>> bind, Func<T, TElement, TResult> bindMap)
        => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

    public static async ValueTask<T> Do<T>(this ValueTask<T> task, Action<T> action)
    {
        var res = await task.ConfigureAwait(false);
        action(res);
        return res;
    }
}
