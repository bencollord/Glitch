namespace Glitch.Functional;

public static class StaticCast<T>
    where T : class
{
    public static T UpFrom<TDerived>(TDerived obj)
        where TDerived : T => obj;

    public static Option<T> TryDownFrom(object obj) => Option.Maybe(obj as T);
}
