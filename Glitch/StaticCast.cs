using Glitch.Functional;
using Glitch.Functional.Results;

namespace Glitch
{
    public static class StaticCast<T>
        where T : class
    {
        public static T UpFrom<TDerived>(TDerived obj)
            where TDerived : T => obj;

        [Obsolete("Will remove dependency on Glitch.Functional at some point")]
        public static Expected<T> TryDownFrom(object obj)
            => Maybe(obj as T)
                  .ExpectOr(Errors.InvalidCast<T>(obj));
    }
}
