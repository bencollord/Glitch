using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional;

public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
{
    public static readonly Unit Value = new();

    public static Unit Ignore<T>(T _) => Value;

    /// <summary>
    /// Bit of a hacky method to chain monadic functions that return Unit in operators
    /// and query expressions. Returns a function that can be used to map a unit valued
    /// monad to a constant <paramref name="value"/> value.
    /// 
    /// Sort of like a unit specific K combinator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Func<Unit, T> MapTo<T>(T value) => _ => value;

    /// <summary>
    /// Convenience method for returning unit from actions in expression-bodied methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    /// <param name="arg"></param>
    /// <returns></returns>
    public static Unit Return<T>(Action<T> action, T arg)
    {
        action(arg);
        return Value;
    }

    /// <summary>
    /// Convenience method for discarding the result and returning unit from functions in expression-bodied methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    /// <param name="arg"></param>
    /// <returns></returns>
    public static Unit Return<T, U>(Func<T, U> action, T arg)
    {
        action(arg);
        return Value;
    }

    public int CompareTo(Unit other) => 0;

    public bool Equals(Unit other) => true;

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Unit;

    public override int GetHashCode() => 0;

    public override string ToString() => "()";

    public T Return<T>(T alternateValue) => alternateValue;

    public T Return<T>(Func<T> alternateValueFunction) => alternateValueFunction();

    public static bool operator ==(Unit x, Unit y) => true;
    public static bool operator !=(Unit x, Unit y) => false;
    public static bool operator >=(Unit x, Unit y) => true;
    public static bool operator >(Unit x, Unit y) => false;
    public static bool operator <=(Unit x, Unit y) => true;
    public static bool operator <(Unit x, Unit y) => false;

    public static implicit operator ValueTuple(Unit _) => new();
    public static implicit operator Unit(ValueTuple _) => Value;

    // Unfortunately, you can't do this with IEnumerable<Unit> because C# won't let you implicitly convert interfaces
    public static implicit operator Unit(Unit[] _) => Value;
    public static implicit operator Unit(List<Unit> _) => Value;
    public static implicit operator Unit(ImmutableList<Unit> _) => Value;
    public static implicit operator Unit(ImmutableArray<Unit> _) => Value;
}
