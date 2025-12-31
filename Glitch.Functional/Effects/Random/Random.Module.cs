using System.Collections.Immutable;
using System.Globalization;

namespace Glitch.Functional.Effects;

/// <summary>
/// Factory methods and functions for the <see cref="Random{T}"/> monad.
/// </summary>
/// <remarks>
/// Unlike other modules here, the functions in this module are implemented as
/// static extension methods to the <see cref="Random"/> type. This is to prevent
/// naming collisions since the .NET class is so generally named and in the System
/// namespace, and thus not easy to disambiguate, especially considering there's not
/// really a better name for this monad.
/// 
/// The monad and the System type are so tightly coupled anyway (in fact, most of these functions
/// started as extension methods to <see cref="Random"/>), so they are semantically quite related.
/// </remarks>
public static class RandomExtensions
{
    private const int ByteMaxValueInclusive = 256;

    private const int MaxHours = 24;
    private const int MaxMinutes = 60;
    private const int MaxSeconds = 60;
    private const int MaxMilliseconds = 1000;
    private const int MaxMicroseconds = 1000;
    private static readonly DateOnly UnixEpoch = new(1970, 1, 1);

    private static readonly ImmutableArray<TimeSpan> UtcOffsets = TimeZoneInfo.GetSystemTimeZones()
                                                                              .Select(tz => tz.BaseUtcOffset)
                                                                              .Distinct()
                                                                              .ToImmutableArray();

    extension(Random)
    {
        public static Random<T> Return<T>(T value) => Random<T>.Return(value);

        public static Random<Random> Generator() => new(rng => rng);

        // Numbers
        // ========================================================================================
        public static Random<int> Int() => new(rng => rng.Next());
        public static Random<int> Int(int max) => new(rng => rng.Next(max));
        public static Random<int> Int(int min, int max) => new(rng => rng.Next(min, max));

        public static Random<short> Short() => Short(short.MinValue, short.MaxValue);
        public static Random<short> Short(short max) => Short(0, max);
        public static Random<short> Short(short min, short max) => Int(min, max).Cast<short>();

        public static Random<long> Long() => new(rng => rng.NextInt64());
        public static Random<long> Long(long max) => new(rng => rng.NextInt64(max));
        public static Random<long> Long(long min, long max) => new(rng => rng.NextInt64(min, max));

        public static Random<float> Float() => new(rng => rng.NextSingle());
        public static Random<float> Float(float max) => Float(0, max);
        public static Random<float> Float(float min, float max) =>
            from whole in Short()
            from frac in Float()
            select whole * frac;

        public static Random<double> Double() => new(rng => rng.NextDouble());
        public static Random<double> Double(double max) => Double(0, max);
        public static Random<double> Double(double min, double max) =>
            from whole in Int()
            from frac in Double()
            select whole * frac;

        public static Random<decimal> Decimal() => Double().Select(x => new decimal(x));
        public static Random<decimal> Decimal(decimal max) => Decimal(0, max);
        public static Random<decimal> Decimal(decimal min, decimal max) =>
            from whole in Int()
            from frac in Decimal()
            select whole * frac;

        public static Random<byte> Byte() => Int(0, ByteMaxValueInclusive).Cast<byte>();
        public static Random<byte> Byte(byte max) => Byte(0, max);
        public static Random<byte> Byte(byte min, byte max) => Int(min, max).Cast<byte>();

        public static Random<byte[]> Bytes(int count) => new(rng =>
        {
            var bytes = new byte[count];
            rng.NextBytes(bytes);
            return bytes;
        });

        public static Random<bool> Bool() => Int().Select(x => x % 2 == 0);

        // Chars
        // ========================================================================================
        public static Random<char> Char() => Char('\x7E');
        public static Random<char> Char(char max) => Char('\x20', max);
        public static Random<char> Char(char min, char max) => Int(min, max).Cast<char>();

        public static Random<char> Letter() =>
            from u in Char('A', 'Z')
            from l in Char('a', 'z')
            from c in Choice(u, l)
            select c;

        public static Random<char> Digit() => Char('0', '9');

        public static Random<char> HexDigit() =>
            from d in Digit()
            from l in Char('A', 'F')
            from h in Choice(d, l)
            select h;

        // Sequences
        // ========================================================================================
        public static Random<IEnumerable<T>> Shuffle<T>(IEnumerable<T> items) => new(rng => items.OrderBy(_ => rng.Next()));

        public static Random<T> Choice<T>(T first, T second) => Bool().Select(flag => flag ? first : second);

        public static Random<T> Choice<T>(params IEnumerable<T> items) => Shuffle(items).Select(i => i.First());

        public static Random<IEnumerable<T>> Choices<T>(int count, params IEnumerable<T> items) => Shuffle(items).Select(i => i.Take(count));

        // Dates and Times
        // ========================================================================================

        public static Random<DateOnly> Date() => Date(UnixEpoch.AddYears(70));
        
        public static Random<DateOnly> Date(DateOnly max) => Date(UnixEpoch, max);
        
        public static Random<DateOnly> Date(DateOnly min, DateOnly max) => Date(CultureInfo.InvariantCulture.Calendar, min, max);

        public static Random<DateOnly> Date(Calendar calendar, DateOnly min, DateOnly max) =>
            from year in Int(min.Year, max.Year)
            from month in min.Year == max.Year
                ? Int(min.Month, max.Month)
                : Int(1, calendar.GetMonthsInYear(year))
            from day in min.Year == max.Year && min.Month == max.Month
                ? Int(min.Day, max.Day)
                : Int(1, calendar.GetDaysInMonth(year, month))
            select new DateOnly(year, month, day);

        public static Random<TimeOnly> Time() =>
            from hour        in Int(MaxHours)
            from minute      in Int(MaxMinutes)
            from second      in Int(MaxSeconds)
            from millisecond in Int(MaxMilliseconds)
            from microsecond in Int(MaxMicroseconds)
            select new TimeOnly(hour, minute, second, millisecond, microsecond);

        public static Random<TimeOnly> Time(TimeOnly max) => Long(max.Ticks).Select(e => new TimeOnly(e));

        public static Random<TimeOnly> Time(TimeOnly min, TimeOnly max) => Long(min.Ticks, max.Ticks).Select(e => new TimeOnly(e));

        public static Random<DateTime> DateTime() =>
            from date in Date()
            from time in Time()
            select new DateTime(date, time);

        public static Random<DateTime> DateTime(DateTime max) => Long(max.Ticks).Select(e => new DateTime(e));

        public static Random<DateTime> DateTime(DateTime min, DateTime max) => Long(min.Ticks, max.Ticks).Select(e => new DateTime(e));

        public static Random<TimeSpan> Duration() => Duration(TimeSpan.Zero, TimeSpan.FromHours(24));

        public static Random<TimeSpan> Duration(TimeSpan max) => Duration(TimeSpan.Zero, max);

        public static Random<TimeSpan> Duration(TimeSpan min, TimeSpan max) => Long(min.Ticks, max.Ticks).Select(e => new TimeSpan(e));

        public static Random<TimeSpan> UtcOffset() => Choice(UtcOffsets);
        
        public static Random<TimeSpan> UtcOffset(TimeSpan max) => Choice(UtcOffsets.Where(x => x < max));
        
        public static Random<TimeSpan> UtcOffset(TimeSpan min, TimeSpan max) => Choice(UtcOffsets.Where(x => x < max && x >= min));
    }
}
