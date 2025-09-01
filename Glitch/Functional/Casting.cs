using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static class StaticCast<T>
        where T : class
    {
        public static T UpFrom<TDerived>(TDerived obj)
            where TDerived : T => obj;

        public static Result<T> TryDownFrom(object obj)
            => Maybe(obj as T)
                  .OkayOrElse(_ => 
                      new InvalidCastException($"Cannot cast {obj} to type {typeof(T)}"));
    }

    public static class DynamicCast<T>
    {
        public static T From<TOther>(TOther obj)
            => obj switch
            {
                T upcast => upcast,
                _ => (T)(dynamic)obj!,
            };

        public static Result<T> Try<TOther>(TOther obj)
            => Effect<TOther, T>.Lift(From).Run(obj);
    }
}
