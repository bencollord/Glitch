using Glitch.Functional.Core;

namespace Glitch.Functional.Effects
{
    public static partial class IOExtensions
    {
        public static IO<TResult> Apply<T, TResult>(this IO<Func<T, TResult>> map, IO<T> value) => value.Apply(map);
    }
}