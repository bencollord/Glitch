using Glitch.Grep.Internal;
using System.Collections;

namespace Glitch.Grep
{
    public class FileGrepQuery : IEnumerable<FileLine>
    {
        private readonly FileInfo file;
        private readonly GrepFilter filter;

        internal FileGrepQuery(FileInfo file, GrepFilter filter)
        {
            this.file = file;
            this.filter = filter;
        }

        public FileGrepQuery FixedString() => WithFilter(filter.AsFixedString());
        public FileGrepQuery RegularExpression() => WithFilter(filter.AsRegularExpression());
        public FileGrepQuery IgnoreCase() => WithFilter(filter.IgnoreCase());
        public FileGrepQuery MatchCase() => WithFilter(filter.AsCaseSensitive());
        public FileGrepQuery MatchWholeLine() => WithFilter(filter.MatchWholeLine());
        public FileGrepQuery AllowPartialMatch() => WithFilter(filter.AllowPartialMatch());
        public FileGrepQuery LocaleAware() => WithFilter(filter.LocaleAware());
        public FileGrepQuery LocaleAgnostic() => WithFilter(filter.LocaleAgnostic());

        public IEnumerator<FileLine> GetEnumerator()
        {
            return new FileTraversal(file, filter.IsMatch);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private FileGrepQuery WithFilter(GrepFilter filter) 
            => filter == this.filter ? this : new FileGrepQuery(file, filter);
    }
}