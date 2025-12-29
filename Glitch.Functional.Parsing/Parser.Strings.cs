namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<T>(IParser<char, T> source)
    {
        public IParser<char, T> Lexeme() => Parse.Lexeme(source);
    }

    extension(IParser<char, char> source)
    {
        /// <summary>
        /// Repeats the current <see cref="Parser{TToken, T}"/> until <paramref name="stop"/>
        /// succeeds, discarding the result of <paramref name="stop"/>.
        /// </summary>
        /// <typeparam name="TStop"></typeparam>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IParser<char, string> Until<TStop>(IParser<char, TStop> stop) => new UntilParser<char, char, TStop>(source, stop).Select(AsString);

        public IManyParser<char, char, string> SeparatedBy<TSeparator>(IParser<char, TSeparator> separator) => new SeparatedParser<char, char, TSeparator, string>(source, separator, AsString);

        public IManyParser<char, char, string> Many() => new ManyParser<char, char, string>(source, AsString);

        public IParser<char, string> Once() => source.Select(x => new string([x]));
        
        public IParser<char, string> AtLeastOnce() => source.Many().AtLeastOnce();
        
        public IParser<char, string> AtLeast(int times) => source.Many().AtLeast(times);
        
        public IParser<char, string> AtMost(int times) => source.Many().AtMost(times);
        
        public IParser<char, string> ZeroOrMoreTimes() => source.Many().ZeroOrMoreTimes();
        
        public IParser<char, string> Times(int count) => source.Many().Times(count);
    }

    private static string AsString(IEnumerable<char> chars) => new string(chars as char[] ?? [.. chars]);
}

