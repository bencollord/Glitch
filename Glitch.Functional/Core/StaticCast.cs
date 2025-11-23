using Glitch.Functional.Errors;

namespace Glitch.Functional
{
    public static class StaticCast<T>
        where T : class
    {
        public static T UpFrom<TDerived>(TDerived obj)
            where TDerived : T => obj;

        public static Expected<T> TryDownFrom(object obj)
            => Option.Maybe(obj as T)
                  .OkayOr(Error.InvalidCast<T>(obj));
    }
}
