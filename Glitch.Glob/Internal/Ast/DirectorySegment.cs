namespace Glitch.Glob.Internal.Ast;

internal class DirectorySegment : Segment
{
    private GlobNode node;
    private Segment next;

    internal DirectorySegment(GlobNode node, Segment next)
    {
        this.node = node;
        this.next = next;
    }

    public override string ToString() => node.ToString();

    internal override IEnumerable<FileSystemInfo> Expand(DirectoryInfo root)
    {
        foreach (var directory in root.EnumerateDirectories())
        {
            if (IsMatch(directory))
            {
                foreach (var nextMatch in next.Expand(directory))
                {
                    yield return nextMatch;
                }
            }
        }
    }

    protected bool IsMatch(DirectoryInfo directory)
    {
        var match = node.Match(directory.Name);

        return match.IsSuccess && match.Length == directory.Name.Length;
    }
}
