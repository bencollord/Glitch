namespace Glitch.Functional
{
    public static class TaskExtensions
    {
        public static Task<TResult> Map<T, TResult>(this Task<T> task, Func<T, TResult> mapper, CancellationToken cancellationToken = default)
            => task.ContinueWith(t => mapper(t.Result), cancellationToken);

        public static Task<TResult> Apply<T, TResult>(this Task<T> task, Task<Func<T, TResult>> function, CancellationToken cancellationToken = default)
            => task.AndThen(v => function.Map(fn => fn(v), cancellationToken), cancellationToken);

        public static Task<TResult> AndThen<T, TResult>(this Task<T> task, Func<T, Task<TResult>> mapper, CancellationToken cancellationToken = default)
            => task.Map(mapper, cancellationToken).Unwrap();

        public static Task<T> Do<T>(this Task<T> task, Action<T> action, CancellationToken cancellationToken = default)
            => task.ContinueWith(t =>
            {
                action(t.Result);
                return t.Result;
            }, cancellationToken);
    }
}
