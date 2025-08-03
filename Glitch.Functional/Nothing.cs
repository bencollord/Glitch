using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public readonly struct Nothing : IEquatable<Nothing>, IComparable<Nothing>
    {
        public static readonly Nothing Value = new();

        public int CompareTo(Nothing other) => 0;

        public bool Equals(Nothing other) => true;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Nothing;

        public override int GetHashCode() => 0;

        public override string ToString() => "()";

        public T Return<T>(T alternateValue) => alternateValue;

        public T Return<T>(Func<T> alternateValueFunction) => alternateValueFunction();

        public static bool operator ==(Nothing x, Nothing y) => true;
        public static bool operator !=(Nothing x, Nothing y) => false;
        public static bool operator >=(Nothing x, Nothing y) => true;
        public static bool operator >(Nothing x, Nothing y) => false;
        public static bool operator <=(Nothing x, Nothing y) => true;
        public static bool operator <(Nothing x, Nothing y) => false;
    }
}
