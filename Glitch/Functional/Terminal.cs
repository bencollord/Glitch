using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public readonly struct Terminal : IEquatable<Terminal>, IComparable<Terminal>
    {
        public static readonly Terminal Value = new();

        public int CompareTo(Terminal other) => 0;

        public bool Equals(Terminal other) => true;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Terminal;

        public override int GetHashCode() => 0;

        public override string ToString() => "()";

        public T Return<T>(T alternateValue) => alternateValue;

        public T Return<T>(Func<T> alternateValueFunction) => alternateValueFunction();

        public static bool operator ==(Terminal x, Terminal y) => true;
        public static bool operator !=(Terminal x, Terminal y) => false;
        public static bool operator >=(Terminal x, Terminal y) => true;
        public static bool operator >(Terminal x, Terminal y) => false;
        public static bool operator <=(Terminal x, Terminal y) => true;
        public static bool operator <(Terminal x, Terminal y) => false;
    }
}
