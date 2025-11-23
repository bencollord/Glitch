namespace Glitch.Functional
{
    internal static class ErrorMessages
    {
        public static string InvalidCast<T>(object? from) => InvalidCast(from, typeof(T));

        public static string InvalidCast(object? from, Type to) => $"Cannot cast '{from ?? "null"}' to type {to}";

        internal static string BadUnwrap<E>(E? error) => $"Attempted to unwrap a faulted result. Error: {error}";

        internal static string BadUnwrapError<T>(T? value) => $"Attempted to unwrap error of okay result. Value: {value}";
    }
}