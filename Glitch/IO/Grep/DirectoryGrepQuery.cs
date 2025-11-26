using Glitch.Grep.Internal;
using System.Collections;

namespace Glitch.Grep;

public class DirectoryGrepQuery : GrepQuery<DirectoryGrepQuery>
{
    private readonly DirectoryTraversal traversal;
    private readonly GrepFilter filter;

    internal DirectoryGrepQuery(DirectoryInfo directory, GrepFilter filter)
        : this(new DirectoryTraversal(directory), filter) { }

    private DirectoryGrepQuery(DirectoryTraversal traversal, GrepFilter filter)
        : base(filter)
    {
        this.traversal = traversal;
        this.filter = filter;
    }

    public DirectoryGrepQuery Include(string pattern) => new(traversal.Include(pattern), filter);

    public DirectoryGrepQuery Recursive() => new(traversal.Recursive(), filter);

    protected override IEnumerable<FileInfo> EnumerateFiles() => traversal.Execute();

    private protected override DirectoryGrepQuery WithFilter(GrepFilter filter) => new(traversal, filter);
}