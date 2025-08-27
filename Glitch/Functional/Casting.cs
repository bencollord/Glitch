using Glitch.Functional.Results;

namespace Glitch.Functional
{
    public static class StaticCast<T>
        where T : class
    {
        public static T Up<TDerived>(TDerived obj)
            where TDerived : T => obj;

        public static Option<T> Down(object obj) => Maybe(obj as T);
    }

    public static class DynamicCast<T>
    {
        public static T From<TOther>(TOther obj)
            => (T)(dynamic)obj!;

        public static Result<T> Try<TOther>(TOther obj)
            => Effect<TOther, T>.Lift(From).Run(obj);
    }
}
