using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
    {
        public static readonly Unit Value = new();

        public static Unit Ignore<T>(T _) => Value;

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
}
