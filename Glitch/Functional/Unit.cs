using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    // TODO Find a better name than this. It may be mathematically accurate, but I don't like it.
    // "Terminal" is what I call it in FN, but that causes naming collisions when you import the module...
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
    {
        public static readonly Unit Value = new();

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
    }
}
