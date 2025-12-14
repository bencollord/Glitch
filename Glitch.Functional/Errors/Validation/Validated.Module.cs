using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

public static partial class Validated
{
    public static Okay<T> Okay<T>() 
        where T : new() 
        => Okay(new T());

    public static Okay<T> Okay<T>(T value) => new(value);

    public static Validated<T, E> Okay<T, E>(T value) => new Validated<T, E>.Okay(value);

    public static Validated<T, E> Warning<T, E>(T value, E error) => new Validated<T, E>.Warning(value, Sequence.Singleton(error));
    
    public static Validated<T, E> Warning<T, E>(T value, params IEnumerable<E> errors) => new Validated<T, E>.Warning(value, Sequence.From(errors));

    public static Fail<E> Fatal<E>(E error) => new(error);

    public static Validated<T, E> Fatal<T, E>(params IEnumerable<E> errors) => new Validated<T, E>.Fatal(Sequence.From(errors));

    public static bool IsOkay<T, E>(Validated<T, E> result) => result.IsOkay;

    public static bool IsWarning<T, E>(Validated<T, E> result) => result.IsWarning;

    public static bool IsFatal<T, E>(Validated<T, E> result) => result.IsFatal;

    public static bool HasValue<T, E>(Validated<T, E> result) => result.HasValue;

    public static bool HasError<T, E>(Validated<T, E> result) => result.HasError;
}