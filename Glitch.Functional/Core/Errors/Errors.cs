namespace Glitch.Functional;


public abstract partial record Error
{

    public static readonly Error NoElements = New(GlobalErrorCode.NoElements, "No elements found");

    public static readonly Error MoreThanOneElement = New(GlobalErrorCode.MoreThanOneElement, "More than one element found");

    public static Error InvalidCast<T>(object? from) => InvalidCast(from, typeof(T));
    public static Error InvalidCast(object? from, Type to) => New(GlobalErrorCode.InvalidCast, new InvalidCastException($"Cannot cast '{from ?? "null"}' to type {to}"));

    internal static Error BadUnwrap<E>(E error) => BadUnwrap($"Attempted to unwrap a faulted result. Error: {error}");
    
    internal static Error BadUnwrapError<T>(T value) => BadUnwrap($"Attempted to unwrap error value of a successful result. Value: {value}");
    
    internal static Error BadUnwrap(string message) => New(GlobalErrorCode.BadUnwrap, new InvalidOperationException(message));
}
