using Glitch.IO;
using System.Collections;
using System.Collections.Immutable;

namespace Glitch.Grep.Internal;

internal class DirectoryTraversal
{
    private readonly DirectoryInfo directory;
    private readonly string pattern;
    private readonly SearchOption searchOption;

    internal DirectoryTraversal(DirectoryInfo directory)
        : this(directory, "*", SearchOption.TopDirectoryOnly) { }

    private DirectoryTraversal(DirectoryInfo directory, string pattern, SearchOption searchOption)
    {
        this.directory = directory;
        this.pattern = pattern;
        this.searchOption = searchOption;
    }

    internal DirectoryTraversal Include(string pattern) => new(directory, pattern, searchOption);

    // TODO Multi includes, excludes, an actual globbing engine

    internal DirectoryTraversal Recursive() => new(directory, pattern, SearchOption.AllDirectories);

    internal IEnumerable<FileInfo> Execute() => directory.EnumerateFiles(pattern, searchOption);
}
