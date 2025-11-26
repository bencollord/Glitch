using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

public static partial class Validated
{
    public static Okay<T> Okay<T>() 
        where T : new() 
        => Okay(new T());

    public static Okay<T> Okay<T>(T value) => new(value);

    public static Validated<T, E> Okay<T, E>(T value) => new Validated<T, E>.Okay(value);
    
    public static Fail<E> Fail<E>(E error) => new(error);

    public static Validated<T, E> Fail<T, E>(params IEnumerable<E> errors) => new Validated<T, E>.Fail(Sequence.From(errors));

    public static bool IsOkay<T, E>(Validated<T, E> result) => result.IsOkay;

    public static bool IsFail<T, E>(Validated<T, E> result) => result.IsFail;

    public static Validated<Unit, E> Guard<E>(bool condition, E error)
        => condition ? new Validated<Unit, E>.Okay(Unit.Value) : new Validated<Unit, E>.Fail(error);

    public static Validated<Unit, E> Guard<E>(bool condition, Func<Unit, E> error)
        => condition ? new Validated<Unit, E>.Okay(Unit.Value) : new Validated<Unit, E>.Fail(error(Unit.Value));

    public static Validated<Unit, E> Guard<E>(Func<Unit, bool> predicate, E error)
        => predicate(Unit.Value) ? new Validated<Unit, E>.Okay(Unit.Value) : new Validated<Unit, E>.Fail(error);

    public static Validated<T, E> Guard<T, E>(bool condition, T value, E error)
        => condition ? new Validated<T, E>.Okay(value) : new Validated<T, E>.Fail(error);

    public static Validated<T, E> Guard<T, E>(bool condition, T value, Func<T, E> error)
        => condition ? new Validated<T, E>.Okay(value) : new Validated<T, E>.Fail(error(value));

    public static Validated<T, E> Guard<T, E>(Func<T, bool> predicate, T value, E error)
        => predicate(value) ? new Validated<T, E>.Okay(value) : new Validated<T, E>.Fail(error);
}