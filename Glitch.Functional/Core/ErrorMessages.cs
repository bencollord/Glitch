namespace Glitch.Functional.Core
{
    internal static class ErrorMessages
    {
        public static string InvalidCast<T>(object? from) => InvalidCast(from, typeof(T));

        public static string InvalidCast(object? from, Type to) => $"Cannot cast '{from ?? "null"}' to type {to}";

    }
}