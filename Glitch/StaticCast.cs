using Glitch.Functional.Results;

namespace Glitch
{
    public static class StaticCast<T>
        where T : class
    {
        public static T UpFrom<TDerived>(TDerived obj)
            where TDerived : T => obj;

        [Obsolete("Will remove dependency on Glitch.Functional at some point")]
        public static Result<T> TryDownFrom(object obj)
            => Maybe(obj as T)
                  .OkayOrElse(_ => 
                      new InvalidCastException($"Cannot cast {obj} to type {typeof(T)}"));
    }
}
