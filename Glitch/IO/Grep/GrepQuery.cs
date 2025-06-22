using Glitch.Grep.Internal;
using Glitch.IO;
using System.Collections;

namespace Glitch.Grep
{
    public abstract class GrepQuery<TDerived> : IEnumerable<FileLine>
        where TDerived : GrepQuery<TDerived>
    {
        private readonly GrepFilter filter;

        private protected GrepQuery(GrepFilter filter)
        {
            this.filter = filter;
        }

        public TDerived FixedString() => WithFilter(filter.FixedString());
        public TDerived RegularExpression() => WithFilter(filter.RegularExpression());
        public TDerived IgnoreCase() => WithFilter(filter.IgnoreCase());
        public TDerived MatchCase() => WithFilter(filter.MatchCase());
        public TDerived MatchWholeLine() => WithFilter(filter.MatchWholeLine());
        public TDerived AllowPartialMatch() => WithFilter(filter.AllowPartialMatch());
        public TDerived LocaleAware() => WithFilter(filter.LocaleAware());
        public TDerived LocaleAgnostic() => WithFilter(filter.LocaleAgnostic());

        public IEnumerator<FileLine> GetEnumerator()
        {
            return EnumerateFiles()
                .SelectMany(filter.Scan)
                .GetEnumerator();
        }

        protected abstract IEnumerable<FileInfo> EnumerateFiles();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private protected abstract TDerived WithFilter(GrepFilter filter);
    }
}