using Glitch.Functional;
using Glitch.Functional.Results;

namespace Glitch
{
    public static class DynamicCast<T>
    {
        public static T From<TOther>(TOther obj)
            => obj switch
            {
                T upcast => upcast,
                _ => (T)(dynamic)obj!,
            };

        [Obsolete("Will remove dependency on Glitch.Functional at some point")]
        public static Expected<T> Try<TOther>(TOther obj)
            => Effect<TOther, T>.Lift(From).Run(obj);
    }
}
