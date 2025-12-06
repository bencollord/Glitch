namespace Glitch.Functional.Effects;

public static partial class RandExtensions
{
    extension<T, TResult>(Random<T> _)
    {
        // Map
        public static Random<TResult> operator *(Random<T> x, Func<T, TResult> map) => x.Select(map);
        public static Random<TResult> operator *(Func<T, TResult> map, Random<T> x) => x.Select(map);

        // Apply
        public static Random<TResult> operator *(Random<T> x, Random<Func<T, TResult>> apply) => x.Apply(apply);
        public static Random<TResult> operator *(Random<Func<T, TResult>> apply, Random<T> x) => x.Apply(apply);

        // Bind
        public static Random<TResult> operator >>>(Random<T> x, Func<T, Random<TResult>> bind) => x.AndThen(bind);
    }
}
