namespace Glitch
{
    public enum Radix
    {
        Binary = 2,
        Octal = 8,
        Decimal = 10,
        Hexadecimal = 16
    }

    public static class NumberExtensions
    {
        public static string ToString(this byte value, int radix) => Convert.ToString(value, radix);
        public static string ToString(this byte value, Radix radix) => Convert.ToString(value, (int)radix);
        public static string ToString(this short value, int radix) => Convert.ToString(value, radix);
        public static string ToString(this short value, Radix radix) => Convert.ToString(value, (int)radix);
        public static string ToString(this int value, int radix) => Convert.ToString(value, radix);
        public static string ToString(this int value, Radix radix) => Convert.ToString(value, (int)radix);
        public static string ToString(this long value, int radix) => Convert.ToString(value, radix);
        public static string ToString(this long value, Radix radix) => Convert.ToString(value, (int)radix);
        public static string ToString(this sbyte value, int radix) => Convert.ToString(value, radix);
        public static string ToString(this sbyte value, Radix radix) => Convert.ToString(value, (int)radix);
        public static string ToString(this ushort value, int radix) => Convert.ToString(value, radix);
        public static string ToString(this ushort value, Radix radix) => Convert.ToString(value, (int)radix);
        public static string ToString(this uint value, int radix) => Convert.ToString(value, radix);
        public static string ToString(this uint value, Radix radix) => Convert.ToString(value, (int)radix);
    }
}
