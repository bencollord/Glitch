namespace Glitch.Numerics
{
    public struct Percentage : IEquatable<Percentage>, IComparable<Percentage>, IFormattable
    {
        private float value;

        public Percentage(float value)
        {
            this.value = value;
        }

        public int CompareTo(Percentage other) => value.CompareTo(other.value);

        public bool Equals(Percentage other) => value == other.value;

        public override bool Equals(object? obj) => obj is Percentage p && Equals(p);

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => ToString("G");

        public string ToString(string? format) => ToString(format, null);

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            var customFormatter = formatProvider?.GetFormat(typeof(ICustomFormatter)) as ICustomFormatter;

            if (customFormatter is not null)
            {
                return customFormatter.Format(format, this, formatProvider);
            }

            // Default formatting
            if (String.IsNullOrEmpty(format) || format.Equals("G", StringComparison.OrdinalIgnoreCase))
            {
                // Don't show trailing zeros for a full percentage
                string defaultFormat = (float)Math.Round(value, 2) == value ? "P0" : "P";

                return value.ToString(defaultFormat, formatProvider);
            }

            // Disallow formatting as currency
            if (format.Equals("C", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException($"Invalid format string '{format}'");
            }

            return value.ToString(format, formatProvider);
        }
    }
}
