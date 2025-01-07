using System.Text.RegularExpressions;

namespace Glitch.Grep.Internal
{
    internal class RegexGrepFilter : GrepFilter
    {
        private Lazy<Regex> regex;

        internal RegexGrepFilter(string pattern, GrepOptions options)
            : base(pattern, options)
        {
            regex = new Lazy<Regex>(BuildRegex);
        }

        internal override GrepFilter AllowPartialMatch()
        {
            if (options.HasFlag(GrepOptions.MatchWholeLine))
            {
                return new RegexGrepFilter(pattern, options.RemoveFlag(GrepOptions.MatchWholeLine));
            }

            return this;
        }

        internal override GrepFilter MatchWholeLine()
        {
            if (!options.HasFlag(GrepOptions.MatchWholeLine))
            {
                return new RegexGrepFilter(pattern, options.AddFlag(GrepOptions.MatchWholeLine));
            }

            return this;
        }

        internal override GrepFilter FixedString()
        {
            return new StringGrepFilter(pattern, options);
        }

        internal override GrepFilter RegularExpression()
        {
            return this;
        }

        internal override GrepFilter IgnoreCase()
        {
            if (!options.HasFlag(GrepOptions.IgnoreCase))
            {
                return new RegexGrepFilter(pattern, options.AddFlag(GrepOptions.IgnoreCase));
            }

            return this;
        }

        internal override GrepFilter MatchCase()
        {
            if (options.HasFlag(GrepOptions.IgnoreCase))
            {
                return new RegexGrepFilter(pattern, options.RemoveFlag(GrepOptions.IgnoreCase));
            }

            return this;
        }

        internal override GrepFilter LocaleAgnostic()
        {
            if (options.HasFlag(GrepOptions.LocaleAware))
            {
                return new RegexGrepFilter(pattern, options.RemoveFlag(GrepOptions.LocaleAware));
            }

            return this;
        }

        internal override GrepFilter LocaleAware()
        {
            if (!options.HasFlag(GrepOptions.LocaleAware))
            {
                return new RegexGrepFilter(pattern, options.AddFlag(GrepOptions.LocaleAware));
            }

            return this;
        }

        internal override bool IsMatch(string line) => regex.Value.IsMatch(line);

        private Regex BuildRegex()
        {
            RegexOptions regexOptions = RegexOptions.None;

            if (options.HasFlag(GrepOptions.IgnoreCase))
            {
                regexOptions |= RegexOptions.IgnoreCase;
            }

            if (!options.HasFlag(GrepOptions.LocaleAware))
            {
                regexOptions |= RegexOptions.CultureInvariant;
            }

            if (options.HasFlag(GrepOptions.MatchWholeLine))
            {
                return new Regex($"^{pattern}$", regexOptions);
            }

            return new Regex(pattern, regexOptions);
        }
    }
}
