namespace Glitch.Functional;

internal static class ErrorMessages
{
    public const string BadDiscriminatedUnion = "If you're seeing this message, a switch on the type of a discriminated "
                                              + "union failed. I don't know how you got past the private constructor and "
                                              + "sealed classes, but this type is not meant to be extended.";

    public static string InvalidCast<T>(object? from) => InvalidCast(from, typeof(T));

    public static string InvalidCast(object? from, Type to) => $"Cannot cast '{from ?? "null"}' to type {to}";

    internal static string BadUnwrap<E>(E? error) => $"Attempted to unwrap a faulted result. Error: {error}";

    internal static string BadUnwrapError<T>(T? value) => $"Attempted to unwrap error of okay result. Value: {value}";
}