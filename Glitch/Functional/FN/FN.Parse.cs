namespace Glitch.Functional
{
    public static partial class FN
    {
        public static Result<T> Parse<T>(string input)
            where T : IParsable<T>
            => Parse<T>(input, None);

        public static Result<T> Parse<T>(string input, Option<IFormatProvider> formatProvider)
            where T : IParsable<T>
            => T.TryParse(input, formatProvider.UnwrapOrDefault(), out var result) ? Okay(result) : ParseError<T>(input);

        public static Result<T> ParseEnum<T>(string input)
            where T : struct, Enum
            => ParseEnum<T>(input, false);

        public static Result<T> ParseEnum<T>(string input, bool ignoreCase)
            where T : struct, Enum
            => Enum.TryParse(input, ignoreCase, out T result) ? Okay(result) : ParseError<T>(input);

        public static Result<bool> ParseBool(string input) => bool.TryParse(input, out var result) ? Okay(result) : ParseError<bool>(input);
        public static Result<byte> ParseByte(string input) => byte.TryParse(input, out var result) ? Okay(result) : ParseError<byte>(input);
        public static Result<char> ParseChar(string input) => char.TryParse(input, out var result) ? Okay(result) : ParseError<char>(input);
        public static Result<DateOnly> ParseDate(string input) => DateOnly.TryParse(input, out var result) ? Okay(result) : ParseError<DateOnly>(input);
        public static Result<DateTime> ParseDateTime(string input) => DateTime.TryParse(input, out var result) ? Okay(result) : ParseError<DateTime>(input);
        public static Result<DateTimeOffset> ParseDateTimeOffset(string input) => DateTimeOffset.TryParse(input, out var result) ? Okay(result) : ParseError<DateTimeOffset>(input);
        public static Result<decimal> ParseDecimal(string input) => decimal.TryParse(input, out var result) ? Okay(result) : ParseError<decimal>(input);
        public static Result<double> ParseDouble(string input) => double.TryParse(input, out var result) ? Okay(result) : ParseError<double>(input);
        public static Result<float> ParseFloat(string input) => float.TryParse(input, out var result) ? Okay(result) : ParseError<float>(input);
        public static Result<Guid> ParseGuid(string input) => Guid.TryParse(input, out var result) ? Okay(result) : ParseError<Guid>(input);
        public static Result<int> ParseInt(string input) => int.TryParse(input, out var result) ? Okay(result) : ParseError<int>(input);
        public static Result<Int128> ParseInt128(string input) => Int128.TryParse(input, out var result) ? Okay(result) : ParseError<Int128>(input);
        public static Result<long> ParseLong(string input) => long.TryParse(input, out var result) ? Okay(result) : ParseError<long>(input);
        public static Result<sbyte> ParseSByte(string input) => sbyte.TryParse(input, out var result) ? Okay(result) : ParseError<sbyte>(input);
        public static Result<short> ParseShort(string input) => short.TryParse(input, out var result) ? Okay(result) : ParseError<short>(input);
        public static Result<TimeOnly> ParseTime(string input) => TimeOnly.TryParse(input, out var result) ? Okay(result) : ParseError<TimeOnly>(input);
        public static Result<TimeSpan> ParseTimeSpan(string input) => TimeSpan.TryParse(input, out var result) ? Okay(result) : ParseError<TimeSpan>(input);
        public static Result<uint> ParseUInt(string input) => uint.TryParse(input, out var result) ? Okay(result) : ParseError<uint>(input);
        public static Result<UInt128> ParseUInt128(string input) => UInt128.TryParse(input, out var result) ? Okay(result) : ParseError<UInt128>(input);
        public static Result<ulong> ParseULong(string input) => ulong.TryParse(input, out var result) ? Okay(result) : ParseError<ulong>(input);
        public static Result<ushort> ParseUShort(string input) => ushort.TryParse(input, out var result) ? Okay(result) : ParseError<ushort>(input);

        private static Result<T> ParseError<T>(string input) => new ParseError(input, typeof(T));
    }
}
