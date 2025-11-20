namespace Glitch.Functional.Errors
{

    public abstract partial record Error
    {

        public static readonly Error NoElements = Error.New(GlobalErrorCode.NoElements, "No elements found");

        public static readonly Error MoreThanOneElement = Error.New(GlobalErrorCode.MoreThanOneElement, "More than one element found");

        public static Error InvalidCast<T>(object? from) => InvalidCast(from, typeof(T));
        public static Error InvalidCast(object? from, Type to) => Error.New(GlobalErrorCode.InvalidCast, new InvalidCastException($"Cannot cast '{from ?? "null"}' to type {to}"));

        internal static Error BadUnwrap<E>(E error) => BadUnwrap($"Attempted to unwrap a faulted result. Error: {error}");
        internal static Error BadUnwrapError<T>(T value) => BadUnwrap($"Attempted to unwrap error value of a successful result. Value: {value}");
        internal static Error KeyNotFound<TKey>(TKey key) => Error.New(GlobalErrorCode.KeyNotFound, $"Key '{key}' was not found");
        
        private static Error BadUnwrap(string message) => Error.New(GlobalErrorCode.BadUnwrap, new InvalidOperationException(message));
    }
}
