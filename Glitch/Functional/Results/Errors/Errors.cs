namespace Glitch.Functional.Results
{
    public enum ErrorCode
    {
        Unspecified        = 0,
        None               = -0xBAD0000,
        Aggregate          = -0xBAD0001,
        NoElements         = -0xBAD0002,
        MoreThanOneElement = -0xBAD0003,
        ParseError         = -0xBAD0004,
        Unexpected         = -0xBAD0005,
        BadUnwrap          = -0xBAD0006,
        KeyNotFound        = -0xBAD0007,
        InvalidCast        = -0xBADCA57,
    }

    public static class Errors
    {
        public static readonly Error NoElements = Error.New(ErrorCode.NoElements, "No elements found");

        public static readonly Error MoreThanOneElement = Error.New(ErrorCode.MoreThanOneElement, "More than one element found");

        public static Error InvalidCast<T>(object? from) => InvalidCast(from, typeof(T));
        public static Error InvalidCast(object? from, Type to) => Error.New(ErrorCode.InvalidCast, new InvalidCastException($"Cannot cast '{from ?? "null"}' to type {to}"));

        internal static Error BadUnwrap<E>(E error) => BadUnwrap($"Attempted to unwrap a faulted result. Error: {error}");
        internal static Error BadUnwrapError<T>(T value) => BadUnwrap($"Attempted to unwrap error value of a successful result. Value: {value}");
        internal static Error KeyNotFound<TKey>(TKey key) => Error.New(ErrorCode.KeyNotFound, $"Key '{key}' was not found");
        
        private static Error BadUnwrap(string message) => Error.New(ErrorCode.BadUnwrap, new InvalidOperationException(message));
    }
}
