namespace Glitch.Functional;

public static partial class FuncExtensions
{
    public static Func<T, bool> AsFunc<T>(this Predicate<T> predicate) => x => predicate(x);
    public static Func<T, TResult> AsFunc<T, TResult>(this Converter<T, TResult> converter) => x => converter(x);

    public static Predicate<T> AsPredicate<T>(this Func<T, bool> predicate) => x => predicate(x);
    public static Converter<T, TResult> AsConverter<T, TResult>(this Func<T, TResult> converter) => x => converter(x);
}