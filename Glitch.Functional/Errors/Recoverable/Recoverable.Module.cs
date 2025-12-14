namespace Glitch.Functional.Errors;

public static partial class Recoverable
{
    public static Okay<T> Okay<T>() 
        where T : new() 
        => Okay(new T());

    public static Okay<T> Okay<T>(T value) => new(value);

    public static Recoverable<T, E> Okay<T, E>(T value) => new Recoverable<T, E>.Okay(value);

    public static Recoverable<T, E> Warning<T, E>(T value, E error) => new Recoverable<T, E>.Warning(value, error);
    
    public static Fail<E> Fatal<E>(E error) => new(error);

    public static Recoverable<T, E> Fatal<T, E>(E error) => new Recoverable<T, E>.Fatal(error);

    public static bool IsOkay<T, E>(Recoverable<T, E> result) => result.IsOkay;

    public static bool IsWarning<T, E>(Recoverable<T, E> result) => result.IsWarning;

    public static bool IsFatal<T, E>(Recoverable<T, E> result) => result.IsFatal;

    public static bool HasValue<T, E>(Recoverable<T, E> result) => result.HasValue;

    public static bool HasError<T, E>(Recoverable<T, E> result) => result.HasError;
}