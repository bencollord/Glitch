using Glitch.Functional;
using Glitch.Functional.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch
{
    public static class DynamicCast<T>
    {
        public static T From<TFrom>(TFrom obj)
            => obj switch
            {
                T upcast => upcast,
                _ => (T)(dynamic)obj!,
            };

        public static bool Try<TFrom>(TFrom obj, out T result)
        {
            try
            {
                result = From(obj);
                return true;
            }
            catch
            {
                result = default!;
                return false;
            }
        }

        [Obsolete("Will remove dependency on Glitch.Functional at some point")]
        public static Expected<T> Try<TFrom>(TFrom obj)
            => Effect<TFrom, T>.Lift(From).Run(obj);
    }
}
