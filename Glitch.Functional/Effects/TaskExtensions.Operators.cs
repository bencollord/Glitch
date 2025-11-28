
namespace Glitch.Functional.Effects;

public static partial class TaskExtensions
{
    extension<T, TResult>(Task<T> _)
    {
        public static Task<TResult> operator *(Task<T> task, Func<T, TResult> map) => task.Select(map);
        public static Task<TResult> operator *(Func<T, TResult> map, Task<T> task) => task.Select(map);

        public static Task<TResult> operator *(Task<T> task, Task<Func<T, TResult>> function) => task.Apply(function);
        public static Task<TResult> operator *(Task<Func<T, TResult>> function, Task<T> task) => task.Apply(function);

        public static Task<TResult> operator >>>(Task<T> task, Task<TResult> other) => task.AndThen(async _ => await other.ConfigureAwait(false));
        public static Task<TResult> operator >>>(Task<T> task, Func<T, Task<TResult>> bind) => task.AndThen(bind);
    }
}
