using System.Collections.Immutable;
using System.Globalization;

namespace Glitch.Functional
{
    public static class Rand
    {
        public static Rand<T> Return<T>(T value) => Rand<T>.Return(value);

        public static Rand<Random> Generator() => new(rng => rng);

        // Numbers
        // ========================================================================================
        public static Rand<int> Int() => new(rng => rng.Next());
        public static Rand<int> Int(int max) => new(rng => rng.Next(max));
        public static Rand<int> Int(int min, int max) => new(rng => rng.Next(min, max));

        public static Rand<short> Short() => Short(short.MinValue, short.MaxValue);
        public static Rand<short> Short(short max) => Short(0, max);
        public static Rand<short> Short(short min, short max) => Int(min, max).Cast<short>();

        public static Rand<long> Long() => new(rng => rng.NextInt64());
        public static Rand<long> Long(long max) => new(rng => rng.NextInt64(max));
        public static Rand<long> Long(long min, long max) => new(rng => rng.NextInt64(min, max));

        public static Rand<float> Float() => new(rng => rng.NextSingle());
        public static Rand<float> Float(float max) => Float(0, max);
        public static Rand<float> Float(float min, float max) =>
            from whole in Short()
            from frac in Float()
            select whole * frac;

        public static Rand<double> Double() => new(rng => rng.NextDouble());
        public static Rand<double> Double(double max) => Double(0, max);
        public static Rand<double> Double(double min, double max) =>
            from whole in Int()
            from frac  in Double()
            select whole * frac;

        public static Rand<decimal> Decimal() => Double().Select(x => new decimal(x));
        public static Rand<decimal> Decimal(decimal max) => Decimal(0, max);
        public static Rand<decimal> Decimal(decimal min, decimal max) =>
            from whole in Int()
            from frac  in Decimal()
            select whole * frac;

        private const int ByteMaxValueInclusive = 256;

        public static Rand<byte> Byte() => Int(0, ByteMaxValueInclusive).Cast<byte>();
        public static Rand<byte> Byte(byte max) => Byte(0, max);
        public static Rand<byte> Byte(byte min, byte max) => Int(min, max).Cast<byte>();

        public static Rand<byte[]> Bytes(int count) => new(rng =>
        {
            var bytes = new byte[count];
            rng.NextBytes(bytes);
            return bytes;
        });

        public static Rand<bool> Bool() => Int().Select(x => x % 2 == 0);

        // Chars
        // ========================================================================================
        public static Rand<char> Char() => Char('\x7E');
        public static Rand<char> Char(char max) => Char('\x20', max);
        public static Rand<char> Char(char min, char max) => Int(min, max).Cast<char>();

        public static Rand<char> Letter() =>
            from u in Char('A', 'Z')
            from l in Char('a', 'z')
            from c in ChooseOne(u, l)
            select c;

        public static Rand<char> Digit() => Char('0', '9');

        public static Rand<char> HexDigit() =>
            from d in Digit()
            from l in Char('A', 'F')
            from h in ChooseOne(d, l)
            select h;

        // Sequences
        // ========================================================================================
        public static Rand<IEnumerable<T>> Shuffle<T>(IEnumerable<T> items) => new(rng => items.OrderBy(_ => rng.Next()));

        public static Rand<T> ChoseOne<T>(T first, T second) => Bool().Select(flag => flag ? first : second);

        public static Rand<T> ChooseOne<T>(params IEnumerable<T> items) => Shuffle(items).Select(i => i.First());

        public static Rand<IEnumerable<T>> Choose<T>(int count, params IEnumerable<T> items) => Shuffle(items).Select(i => i.Take(count));

        // Dates and Times
        // ========================================================================================
        private static readonly DateOnly UnixEpoch = new(1970, 1, 1);

        public static Rand<DateOnly> Date() => Date(UnixEpoch.AddYears(70));
        public static Rand<DateOnly> Date(DateOnly max) => Date(UnixEpoch, max);
        public static Rand<DateOnly> Date(DateOnly min, DateOnly max) => Date(CultureInfo.InvariantCulture.Calendar, min, max);

        private static Rand<DateOnly> Date(Calendar calendar, DateOnly min, DateOnly max) =>
            from year in Int(min.Year, max.Year)
            from month in min.Year == max.Year
                ? Int(min.Month, max.Month)
                : Int(1, calendar.GetMonthsInYear(year))
            from day in min.Year == max.Year && min.Month == max.Month
                ? Int(min.Day, max.Day)
                : Int(1, calendar.GetDaysInMonth(year, month))
            select new DateOnly(year, month, day);

        private const int MaxHours = 24;
        private const int MaxMinutes = 60;
        private const int MaxSeconds = 60;
        private const int MaxMilliseconds = 1000;
        private const int MaxMicroseconds = 1000;

        public static Rand<TimeOnly> Time() =>
            from hour        in Int(MaxHours)
            from minute      in Int(MaxMinutes)
            from second      in Int(MaxSeconds)
            from millisecond in Int(MaxMilliseconds)
            from microsecond in Int(MaxMicroseconds)
            select new TimeOnly(hour, minute, second, millisecond, microsecond);

        public static Rand<TimeOnly> Time(TimeOnly max) => Long(max.Ticks).Select(e => new TimeOnly(e));

        public static Rand<TimeOnly> Time(TimeOnly min, TimeOnly max) => Long(min.Ticks, max.Ticks).Select(e => new TimeOnly(e));

        public static Rand<DateTime> DateTime() =>
            from date in Date()
            from time in Time()
            select new DateTime(date, time);

        public static Rand<DateTime> DateTime(DateTime max) => Long(max.Ticks).Select(e => new DateTime(e));

        public static Rand<DateTime> DateTime(DateTime min, DateTime max) => Long(min.Ticks, max.Ticks).Select(e => new DateTime(e));

        public static Rand<TimeSpan> Duration() => Duration(TimeSpan.Zero, TimeSpan.FromHours(24));

        public static Rand<TimeSpan> Duration(TimeSpan max) => Duration(TimeSpan.Zero, max);

        public static Rand<TimeSpan> Duration(TimeSpan min, TimeSpan max) => Long(min.Ticks, max.Ticks).Select(e => new TimeSpan(e));

        private static readonly ImmutableArray<TimeSpan> UtcOffsets = TimeZoneInfo.GetSystemTimeZones()
                                                                                  .Select(tz => tz.BaseUtcOffset)
                                                                                  .Distinct()
                                                                                  .ToImmutableArray();

        public static Rand<TimeSpan> UtcOffset() => ChooseOne(UtcOffsets);
        public static Rand<TimeSpan> UtcOffset(TimeSpan max) => ChooseOne(UtcOffsets.Where(x => x < max));
        public static Rand<TimeSpan> UtcOffset(TimeSpan min, TimeSpan max) => ChooseOne(UtcOffsets.Where(x => x < max && x >= min));
    }
}
