using Glitch.Functional;
using Glitch.Functional.Results;
using Glitch.IO;
using System.Numerics;

namespace Glitch.IO
{
    public readonly struct ByteSize 
        : IEquatable<ByteSize>, 
          IComparable<ByteSize>, 
          IFormattable,
          IComparisonOperators<ByteSize, ByteSize, bool>,
          IAdditionOperators<ByteSize, ByteSize, ByteSize>,
          ISubtractionOperators<ByteSize, ByteSize, ByteSize>,
          IMultiplyOperators<ByteSize, ByteSize, ByteSize>,
          IDivisionOperators<ByteSize, ByteSize, ByteSize>
    {
        private const int SizeMultiplier = 1024;
        
        public const int BitsInByte = 8;
        public const int BytesInKilobyte = SizeMultiplier;
        public const int BytesInMegabyte = BytesInKilobyte * SizeMultiplier;
        public const int BytesInGigabyte = BytesInMegabyte * SizeMultiplier;

        public static readonly ByteSize Zero = new(0);

        private readonly long value;

        public ByteSize(long value)
        {
            this.value = value;
        }

        private ByteSize(double value)
        {
            this.value = (long)Math.Round(value);
        }

        public long Bits => value * BitsInByte;
        public long Bytes => value;
        public double Kilobytes => (double)value / BytesInKilobyte;
        public double Megabytes => (double)value / BytesInMegabyte;
        public double Gigabytes => (double)value / BytesInGigabyte;

        public static ByteSize FromBits(long value) => new(value / BitsInByte);
        public static ByteSize FromBytes(long value) => new(value);
        public static ByteSize FromKilobytes(double value) => new(value * BytesInKilobyte);
        public static ByteSize FromMegabytes(double value) => new(value * BytesInMegabyte);
        public static ByteSize FromGigabytes(double value) => new(value * BytesInGigabyte);

        public ByteSize AddBits(long bits) => FromBits(Bits + bits);
        public ByteSize AddBytes(long bytes) => FromBits(Bytes + bytes);
        public ByteSize AddKilobytes(double kilobytes) => FromKilobytes(Kilobytes + kilobytes);
        public ByteSize AddMegabytes(double megabytes) => FromMegabytes(Megabytes + megabytes);
        public ByteSize AddGigabytes(double gigabytes) => FromGigabytes(Gigabytes + gigabytes);

        public ByteSize SubtractBits(long bits) => AddBits(-bits);
        public ByteSize SubtractBytes(long bytes) => AddBytes(-bytes);
        public ByteSize SubtractKilobytes(double kilobytes) => AddKilobytes(-kilobytes);
        public ByteSize SubtractMegabytes(double megabytes) => AddMegabytes(-megabytes);
        public ByteSize SubtractGigabytes(double gigabytes) => AddGigabytes(-gigabytes);

        public bool Equals(ByteSize other) => value.Equals(other.value);

        public int CompareTo(ByteSize other) => value.CompareTo(other.value);

        public override string ToString() => value.ToString();

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (formatProvider?.GetFormat(typeof(ICustomFormatter)) is ICustomFormatter fmt)
            {
                return fmt.Format(format, this, formatProvider);
            }

            if (format is null || format.Equals("G", StringComparison.OrdinalIgnoreCase))
            {
                return ToString();
            }

            return FormatSpecifier.Parse(format).Format(this, formatProvider);
        }

        public override bool Equals(object? obj) 
            => obj is ByteSize other && Equals(other);

        public override int GetHashCode()
            => value.GetHashCode();

        public static bool operator ==(ByteSize left, ByteSize right) => left.Equals(right);

        public static bool operator !=(ByteSize left, ByteSize right) => !(left == right);

        public static bool operator <(ByteSize left, ByteSize right) => left.CompareTo(right) < 0;

        public static bool operator <=(ByteSize left, ByteSize right) => left.CompareTo(right) <= 0;

        public static bool operator >(ByteSize left, ByteSize right) => left.CompareTo(right) > 0;

        public static bool operator >=(ByteSize left, ByteSize right) => left.CompareTo(right) >= 0;

        public static ByteSize operator +(ByteSize left, ByteSize right) 
            => FromBytes(left.Bytes + right.Bytes);

        public static ByteSize operator -(ByteSize left, ByteSize right)
            => FromBytes(left.Bytes - right.Bytes);

        public static ByteSize operator *(ByteSize left, ByteSize right)
            => FromBytes(left.Bytes * right.Bytes);


        public static ByteSize operator /(ByteSize left, ByteSize right)
            => FromBytes(left.Bytes / right.Bytes);


        private double GetValue(Denomination denomination)
        {
            return denomination switch
            {
                Denomination.Bits      => Bits,
                Denomination.Bytes     => Bytes,
                Denomination.Kilobytes => Kilobytes,
                Denomination.Megabytes => Megabytes,
                Denomination.Gigabytes => Gigabytes,
                _                      => Bytes
            };
        }

        private enum Denomination
        {
            Bits,
            Bytes,
            Kilobytes,
            Megabytes,
            Gigabytes
        }


        //Size specifiers
        //Bi bits
        //By bytes
        //Kb kilobytes
        //Mb megabytes
        //Gb gigabytes

        //Supported numerics
        //B binary
        //D decimal (no commas or decimal points)
        //F fixed point (no commas, yes decimal points)
        //N number (commas and decimal points)
        //X hex
        private class FormatSpecifier
        {
            private static readonly Dictionary<string, Denomination> DenominationMap = new(StringComparer.OrdinalIgnoreCase)
            {
                ["BI"] = Denomination.Bits,
                ["BY"] = Denomination.Bytes,
                ["KB"] = Denomination.Kilobytes,
                ["MB"] = Denomination.Megabytes,
                ["GB"] = Denomination.Gigabytes
            };

            private static readonly char[] SupportedNumberFormats = ['B', 'D', 'F', 'N', 'X'];

            private Denomination denomination;
            private Option<string> numberFormat;

            private FormatSpecifier(Denomination denomination, Option<string> numberFormat)
            {
                this.denomination = denomination;
                this.numberFormat = numberFormat;
            }

            internal static FormatSpecifier Parse(string format)
            {
                if (format.Length == 2)
                {
                    return new FormatSpecifier(DenominationMap[format], None);
                }

                string denomination = format[..2];
                var numberFormat = Some(format[2..].Trim())
                    .Filter(f => !string.IsNullOrEmpty(f));

                numberFormat.Do(ValidateNumberFormat);

                return new FormatSpecifier(DenominationMap[denomination], numberFormat);
            }

            internal string Format(ByteSize byteSize, IFormatProvider? formatProvider)
            {
                bool IsFractionalFormat(string fmt)
                    => fmt.StartsWith("F", StringComparison.OrdinalIgnoreCase)
                    || fmt.StartsWith("N", StringComparison.OrdinalIgnoreCase);

                // TODO Fix me
                return numberFormat
                    .Select(fmt => IsFractionalFormat(fmt) 
                             ? byteSize.GetValue(denomination).ToString(fmt, formatProvider)
                             : Convert.ToInt64(byteSize.GetValue(denomination)).ToString(fmt, formatProvider))
                    .IfNone(byteSize.GetValue(denomination).ToString());
            }

            private static void ValidateNumberFormat(string format)
            {
                if (!SupportedNumberFormats.Contains(format[0].ToUpperInvariant()))
                {
                    throw new FormatException($"'{format}' is not supported for ByteSizes.");
                }
            }
        }
    }

    public static class ByteSizeExtensions
    {
        public static ByteSize Average(this IEnumerable<ByteSize> source)
            => ByteSize.FromBytes(Convert.ToInt64(source.Average(s => s.Bytes)));

        public static ByteSize Average<T>(this IEnumerable<T> source, Func<T, ByteSize> selector)
            => source.Select(selector).Average();

        public static ByteSize Min(this IEnumerable<ByteSize> source)
            => ByteSize.FromBytes(source.Select(s => s.Bytes).Min());

        public static ByteSize Min<T>(this IEnumerable<T> source, Func<T, ByteSize> selector)
            => source.Select(selector).Min();

        public static ByteSize Max(this IEnumerable<ByteSize> source)
            => ByteSize.FromBytes(source.Select(s => s.Bytes).Max());
        
        public static ByteSize Max<T>(this IEnumerable<T> source, Func<T, ByteSize> selector)
            => source.Select(selector).Max();

        public static ByteSize Sum(this IEnumerable<ByteSize> source)
            => ByteSize.FromBytes(source.Select(s => s.Bytes).Sum());

        public static ByteSize Sum<T>(this IEnumerable<T> source, Func<T, ByteSize> selector)
            => source.Select(selector).Sum();
    }
}
