using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Numerics
{
    public struct Angle : IComparable<Angle>, IEquatable<Angle>, IFormattable
    {
        private const char DegreeSymbol = '\u00B0';
        private const float MaxDegrees = 360;

        private float degrees;

        public Angle(float degrees)
        {
            if (degrees > MaxDegrees)
            {
                throw new ArgumentException($"Invalid degree amount '{degrees}'");
            }

            this.degrees = degrees;
        }

        public int CompareTo(Angle other) => degrees.CompareTo(other.degrees);

        public bool Equals(Angle other) => degrees == other.degrees;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Angle other && Equals(other);

        public override int GetHashCode() => degrees.GetHashCode();

        public override string ToString() => ToString("G", null);

        public string ToString(string? format) => ToString(format, null);

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            // TODO Validate format string to disallow dumb stuff like currency
            return degrees.ToString(format, formatProvider) + DegreeSymbol;
        }

        public static Angle operator +(Angle x, Angle y) => new Angle((x.degrees + y.degrees) % MaxDegrees);

        public static bool operator ==(Angle x, Angle y) => x.Equals(y);
        public static bool operator !=(Angle x, Angle y) => !x.Equals(y);
        public static bool operator >(Angle x, Angle y) => x.CompareTo(y) > 0;
        public static bool operator >=(Angle x, Angle y) => x.CompareTo(y) >= 0;
        public static bool operator <(Angle x, Angle y) => x.CompareTo(y) < 0;
        public static bool operator <=(Angle x, Angle y) => x.CompareTo(y) <= 0;
    }
}
