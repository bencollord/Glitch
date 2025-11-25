namespace Glitch.Functional;

public readonly partial struct Option<T>
{
    public static implicit operator Option<T>(T? value) => Maybe(value);

    public static implicit operator Option<T>(OptionNone _) => new();

    public static bool operator ==(Option<T> x, Option<T> y) => x.Equals(y);

    public static bool operator >=(Option<T> x, Option<T> y) => x.CompareTo(y) >= 0;

    public static bool operator <=(Option<T> x, Option<T> y) => x.CompareTo(y) <= 0;

    public static bool operator >(Option<T> x, Option<T> y) => x.CompareTo(y) > 0;

    public static bool operator <(Option<T> x, Option<T> y) => x.CompareTo(y) < 0;

    public static bool operator !=(Option<T> x, Option<T> y) => !(x == y);
}