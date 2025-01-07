namespace Glitch.Grep.Internal
{
    internal abstract class GrepFilter
    {
        protected readonly string pattern;
        protected readonly GrepOptions options;

        protected GrepFilter(string pattern, GrepOptions options)
        {
            this.pattern = pattern;
            this.options = options;
        }

        internal abstract GrepFilter AsFixedString();
        internal abstract GrepFilter AsRegularExpression();
        internal abstract GrepFilter IgnoreCase();
        internal abstract GrepFilter AsCaseSensitive();
        internal abstract GrepFilter MatchWholeLine();
        internal abstract GrepFilter AllowPartialMatch();
        internal abstract GrepFilter LocaleAware();
        internal abstract GrepFilter LocaleAgnostic();

        internal abstract bool IsMatch(string line);
    }
}
