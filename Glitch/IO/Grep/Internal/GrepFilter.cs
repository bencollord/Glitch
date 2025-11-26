using Glitch.IO;

namespace Glitch.Grep.Internal;

internal abstract class GrepFilter
{
    protected readonly string pattern;
    protected readonly GrepOptions options;

    protected GrepFilter(string pattern, GrepOptions options)
    {
        this.pattern = pattern;
        this.options = options;
    }

    internal static GrepFilter Create(string pattern, GrepOptions options)
        => options.HasFlag(GrepOptions.FixedString) 
        ? new StringGrepFilter(pattern, options)
        : new RegexGrepFilter(pattern, options);

    internal abstract GrepFilter FixedString();
    internal abstract GrepFilter RegularExpression();
    internal abstract GrepFilter IgnoreCase();
    internal abstract GrepFilter MatchCase();
    internal abstract GrepFilter MatchWholeLine();
    internal abstract GrepFilter AllowPartialMatch();
    internal abstract GrepFilter LocaleAware();
    internal abstract GrepFilter LocaleAgnostic();

    internal IEnumerable<FileLine> Scan(FileInfo file)
    {
        int lineNumber = 1;

        foreach (var line in file.ReadLines())
        {
            if (IsMatch(line.Trim()))
            {
                yield return new FileLine(file, lineNumber++, line.Trim());
            }
        }
    }

    protected abstract bool IsMatch(string line);
}
