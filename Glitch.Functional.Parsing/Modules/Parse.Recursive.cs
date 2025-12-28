namespace Glitch.Functional.Parsing;

public partial class Parse<TToken>
{
    public static IParser<TToken, T> Ref<T>(Func<IParser<TToken, T>> function) => Ref(new Lazy<IParser<TToken, T>>(function));

    public static IParser<TToken, T> Ref<T>(Lazy<IParser<TToken, T>> lazy) => new LazyParser<TToken, T>(lazy);

    /// <summary>
    /// Returns a parser that self-references, allowing recursion.
    /// </summary>
    /// <remarks>
    /// Much like the similar method in Pidgin that inspired this code,
    /// essentially acts as the Y combinator.
    /// </remarks>
    /// <typeparam name="TToken"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="function"></param>
    /// <returns></returns>
    public static IParser<TToken, T> Rec<T>(Func<IParser<TToken, T>, IParser<TToken, T>> function)
    {
        IParser<TToken, T> result = null!;

        result = Ref(() => function(result));

        return result;
    }
}
