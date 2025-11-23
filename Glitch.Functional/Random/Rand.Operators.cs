using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Random;

public static partial class RandExtensions
{
    extension<T, TResult>(Rand<T> _)
    {
        // Map
        public static Rand<TResult> operator *(Rand<T> x, Func<T, TResult> map) => x.Select(map);
        public static Rand<TResult> operator *(Func<T, TResult> map, Rand<T> x) => x.Select(map);

        // Apply
        public static Rand<TResult> operator *(Rand<T> x, Rand<Func<T, TResult>> apply) => x.Apply(apply);
        public static Rand<TResult> operator *(Rand<Func<T, TResult>> apply, Rand<T> x) => x.Apply(apply);

        // Bind
        public static Rand<TResult> operator >>(Rand<T> x, Func<T, Rand<TResult>> bind) => x.AndThen(bind);
    }
}
