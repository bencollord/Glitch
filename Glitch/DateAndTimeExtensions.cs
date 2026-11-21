namespace Glitch;

public static class DateAndTimeExtensions
{
    extension(IEnumerable<TimeSpan> source)
    {
        public TimeSpan Min() => TimeSpan.FromTicks(source.Min(x => x.Ticks));
        public TimeSpan Max() => TimeSpan.FromTicks(source.Max(x => x.Ticks));
        public TimeSpan Sum() => TimeSpan.FromTicks(source.Sum(x => x.Ticks));
        public TimeSpan Average() => TimeSpan.FromMicroseconds(source.Average(x => x.Microseconds));
    }

    extension<T>(IEnumerable<T> source)
    {
        public TimeSpan Min(Func<T, TimeSpan> selector) => source.Select(selector).Min();
        public TimeSpan Max(Func<T, TimeSpan> selector) => source.Select(selector).Max();
        public TimeSpan Sum(Func<T, TimeSpan> selector) => source.Select(selector).Sum();
        public TimeSpan Average(Func<T, TimeSpan> selector) => source.Select(selector).Average();
    }
}
