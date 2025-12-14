namespace Glitch.Functional;

public static partial class OptionExtensions
{
    public static Option<T> Flatten<T>(this Option<Option<T>> source) => source.AndThen(Identity);
}