namespace Glitch.Functional;

public partial record Result<T, E>
{
    public static bool operator true(Result<T, E> result) => result.IsOkay;

    public static bool operator false(Result<T, E> result) => result.IsFail;

    public static implicit operator Result<T, E>(T value) => new Okay(value);

    public static implicit operator Result<T, E>(Okay<T> success) => new Okay(success.Value);

    public static implicit operator Result<T, E>(E error) => new Fail(error);

    public static implicit operator Result<T, E>(Fail<E> failure) => new Fail(failure.Error);

    public static explicit operator T(Result<T, E> result)
        => result.Match(Identity, e => throw new InvalidCastException(ErrorMessages.InvalidCast<T>(e)));

    public static explicit operator E(Result<T, E> result)
        => result.Match(v => throw new InvalidCastException(ErrorMessages.InvalidCast<E>(result)), Identity);
}