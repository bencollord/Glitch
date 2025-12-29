using System.Collections.Immutable;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T, TCollection>(IManyParser<TToken, T, TCollection> source)
        where TCollection : IEnumerable<T>
    {
        public IParser<TToken, TCollection> AtLeastOnce() => source.AtLeast(1);
    }

    extension<TToken, T>(IParser<TToken, T> source)
    {
        /// <summary>
        /// Repeats the current <see cref="Parser{TToken, T}"/> until <paramref name="stop"/>
        /// succeeds, discarding the result of <paramref name="stop"/>.
        /// </summary>
        /// <typeparam name="TStop"></typeparam>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IParser<TToken, IEnumerable<T>> Until<TStop>(IParser<TToken, TStop> stop) => new UntilParser<TToken, T, TStop>(source, stop);

        public IManyParser<TToken, T, IEnumerable<T>> SeparatedBy<TSeparator>(IParser<TToken, TSeparator> separator) => new SeparatedParser<TToken, T, TSeparator, IEnumerable<T>>(source, separator, FN.Identity);

        public IManyParser<TToken, T, IEnumerable<T>> Many() => new ManyParser<TToken, T, IEnumerable<T>>(source, FN.Identity);

        public IParser<TToken, IEnumerable<T>> Once() => source.Select(x => Enumerable.Repeat(x, 1));

        public IParser<TToken, IEnumerable<T>> AtLeastOnce() => source.Many().AtLeastOnce();

        public IParser<TToken, IEnumerable<T>> AtLeast(int times) => source.Many().AtLeast(times);

        public IParser<TToken, IEnumerable<T>> AtMost(int times) => source.Many().AtMost(times);

        public IParser<TToken, IEnumerable<T>> ZeroOrMoreTimes() => source.Many().ZeroOrMoreTimes();

        public IParser<TToken, IEnumerable<T>> Times(int count) => source.Many().Times(count);
    }
}

