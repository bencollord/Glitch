using Glitch.Functional.Errors;

namespace Glitch.Functional;

public readonly partial struct Option<T>
{
    public static implicit operator Option<T>(T? value) => Maybe(value);

    public static implicit operator Option<T>(Nothing _) => new();

    public static explicit operator T(Option<T> option) => option.OkayOr(Error.InvalidCast<T>(option)).IfFail(err => err.Throw<T>());

    public static bool operator ==(Option<T> x, Option<T> y) => x.Equals(y);

    public static bool operator >=(Option<T> x, Option<T> y) => x.CompareTo(y) >= 0;

    public static bool operator <=(Option<T> x, Option<T> y) => x.CompareTo(y) <= 0;

    public static bool operator >(Option<T> x, Option<T> y) => x.CompareTo(y) > 0;

    public static bool operator <(Option<T> x, Option<T> y) => x.CompareTo(y) < 0;

    public static bool operator !=(Option<T> x, Option<T> y) => !(x == y);
}