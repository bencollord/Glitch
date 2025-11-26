using Glitch.Grep.Internal;

namespace Glitch.Grep;

public static class GrepExtensions
{
    public static FileGrepQuery Grep(this FileInfo file, string pattern, GrepOptions options = GrepOptions.None)
        => new(file, GrepFilter.Create(pattern, options));

    public static DirectoryGrepQuery Grep(this DirectoryInfo directory, string pattern, GrepOptions options = GrepOptions.None)
    {
        var query = new DirectoryGrepQuery(directory, GrepFilter.Create(pattern, options));

        // HACK I don't want to deal with this design issue right now
        return options.HasFlag(GrepOptions.Recursive)
            ? query.Recursive()
            : query;
    }
}