using Glitch.IO;

namespace Glitch.IO;

public static class ByteSizeExtensions
{
    extension(IEnumerable<ByteSize> source)
    {
        public ByteSize Average()
        => ByteSize.FromBytes(Convert.ToInt64(source.Average(s => s.Bytes)));

        public ByteSize Min()
            => ByteSize.FromBytes(source.Select(s => s.Bytes).Min());

        public ByteSize Max()
            => ByteSize.FromBytes(source.Select(s => s.Bytes).Max());

        public ByteSize Sum()
            => ByteSize.FromBytes(source.Select(s => s.Bytes).Sum());
    }

    extension<T>(IEnumerable<T> source)
    {
        public ByteSize Average(Func<T, ByteSize> selector)
        => source.Select(selector).Average();

        public ByteSize Min(Func<T, ByteSize> selector)
            => source.Select(selector).Min();

        public ByteSize Max(Func<T, ByteSize> selector)
            => source.Select(selector).Max();

        public ByteSize Sum(Func<T, ByteSize> selector)
            => source.Select(selector).Sum();
    }
}
