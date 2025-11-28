
namespace Glitch.Functional.Effects;

public static partial class ValueTaskExtensions
{
    extension<T, TResult>(ValueTask<T> _)
    {
        public static ValueTask<TResult> operator *(ValueTask<T> task, Func<T, TResult> map) => task.Select(map);
        public static ValueTask<TResult> operator *(Func<T, TResult> map, ValueTask<T> task) => task.Select(map);

        public static ValueTask<TResult> operator *(ValueTask<T> task, ValueTask<Func<T, TResult>> function) => task.Apply(function);
        public static ValueTask<TResult> operator *(ValueTask<Func<T, TResult>> function, ValueTask<T> task) => task.Apply(function);

        public static ValueTask<TResult> operator >>>(ValueTask<T> task, ValueTask<TResult> other) => task.AndThen(async _ => await other.ConfigureAwait(false));
        public static ValueTask<TResult> operator >>>(ValueTask<T> task, Func<T, ValueTask<TResult>> bind) => task.AndThen(bind);
    }
}
