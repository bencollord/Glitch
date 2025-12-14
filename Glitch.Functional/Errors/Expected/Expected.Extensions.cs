namespace Glitch.Functional.Errors;

public static partial class ExpectedExtensions
{
    public static Expected<T> Flatten<T>(this Expected<Expected<T>> source) => source.AndThen(Identity);
}