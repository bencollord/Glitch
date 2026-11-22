using System.Collections.Immutable;

namespace Glitch.Functional.Validation;

public static partial class Validated
{
    public static Okay<T> Okay<T>() 
        where T : new() 
        => Okay(new T());

    public static Okay<T> Okay<T>(T value) => new(value);

    public static Validated<T, E> Okay<T, E>(T value) => Okay(value);
    
    public static Fail<ImmutableArray<E>> Fail<E>(E error) => new([error]);

    public static Fail<ImmutableArray<E>> Fail<E>(ImmutableArray<E> errors) => new(errors);

    public static Fail<ImmutableArray<E>> Fail<E>(params IEnumerable<E> errors) => new([.. errors]);

    public static Validated<T, E> Fail<T, E>(params IEnumerable<E> errors) => Fail(ImmutableArray.CreateRange(errors));

    public static Validated<T, E> From<T, E>(Result<T, E> result) =>
        result switch
        {
            Okay<T> ok => ok,
            Fail<E>(var e) => Fail(e)
        };

    public static Validated<T, E> From<T, E>(Result<T, ImmutableList<E>> result) => result.Match(Okay<T, E>, Fail<T, E>);

    public static bool IsOkay<T, E>(Validated<T, E> result) => result.IsOkay;

    public static bool IsFail<T, E>(Validated<T, E> result) => result.IsFail;

    public static Validated<Unit, E> Guard<E>(bool condition, E error)
        => condition ? Okay(Unit.Value) : Fail(error);

    public static Validated<Unit, E> Guard<E>(bool condition, Func<Unit, E> error)
        => condition ? Okay(Unit.Value) : Fail(error(Unit.Value));

    public static Validated<Unit, E> Guard<E>(Func<Unit, bool> predicate, E error)
        => predicate(Unit.Value) ? Okay(Unit.Value) : Fail(error);

    public static Validated<T, E> Guard<T, E>(bool condition, T value, E error)
        => condition ? Okay(value) : Fail(error);

    public static Validated<T, E> Guard<T, E>(bool condition, T value, Func<T, E> error)
        => condition ? Okay(value) : Fail(error(value));

    public static Validated<T, E> Guard<T, E>(Func<T, bool> predicate, T value, E error)
        => predicate(value) ? Okay(value) : Fail(error);
}