
namespace Glitch.Functional.Effects;

public static partial class LazyExtensions
{
    extension<T, TResult>(Lazy<T> _)
    {
        public static Lazy<TResult> operator *(Lazy<T> task, Func<T, TResult> map) => task.Select(map);
        public static Lazy<TResult> operator *(Func<T, TResult> map, Lazy<T> task) => task.Select(map);

        public static Lazy<TResult> operator %(Lazy<T> task, Lazy<Func<T, TResult>> function) => task.Apply(function);
        public static Lazy<TResult> operator %(Lazy<Func<T, TResult>> function, Lazy<T> task) => task.Apply(function);

        public static Lazy<TResult> operator >>>(Lazy<T> task, Lazy<TResult> other) => task.AndThen(_ => other);
        public static Lazy<TResult> operator >>>(Lazy<T> task, Func<T, Lazy<TResult>> bind) => task.AndThen(bind);
    }
}
