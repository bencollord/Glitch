using Glitch.Functional.Collections;

namespace Glitch.Functional.Validation;

public partial record Validated<T, E>
{
    public static bool operator true(Validated<T, E> result) => result.IsOkay;

    public static bool operator false(Validated<T, E> result) => result.IsFail;

    public static implicit operator Validated<T, E>(T value) => new Okay(value);

    public static implicit operator Validated<T, E>(Okay<T> success) => new Okay(success.Value);

    public static implicit operator Validated<T, E>(E error) => new Fail(error);

    public static implicit operator Validated<T, E>(Fail<E> failure) => new Fail(failure.Error);

    public static implicit operator Validated<T, E>(Result<T, E> result) => Validated.From(result);

    public static implicit operator Validated<T, E>(Result<T, Sequence<E>> result) => Validated.From(result);
}