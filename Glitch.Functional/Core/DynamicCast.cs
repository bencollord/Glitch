namespace Glitch.Functional;

public static class DynamicCast<T>
{
    public static T From<TFrom>(TFrom obj)
        => obj switch
        {
            T upcast => upcast,
            _ => (T)(dynamic)obj!,
        };

    public static Option<T> Try<TFrom>(TFrom obj)
    {
        try
        {
            return Option.Some(From(obj));
        }
        catch
        {
            return Option.None;
        }
    }
}
