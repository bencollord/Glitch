namespace Glitch.Glob.Internal.Ast;

internal class TerminalSegment : Segment
{
    // TODO Pull up to base class?
    private GlobNode node;
    private GlobOptions options;

    internal TerminalSegment(GlobNode node, GlobOptions options)
    {
        this.node = node;
        this.options = options;
    }

    public override string ToString() => node.ToString();

    internal override IEnumerable<FileSystemInfo> Expand(DirectoryInfo root)
    {
        foreach (var fsNode in GetCandidateNodes(root))
        {
            var match = node.Match(fsNode.Name);

            if (match.IsSuccess && match.Length == fsNode.Name.Length)
            {
                yield return fsNode;
            }
        }
    }

    private IEnumerable<FileSystemInfo> GetCandidateNodes(DirectoryInfo root)
    {
        if (options.HasFlag(GlobOptions.ExcludeFiles))
        {
            return root.EnumerateDirectories();
        }

        if (options.HasFlag(GlobOptions.ExcludeDirectories))
        {
            return root.EnumerateFiles();
        }

        return root.EnumerateFileSystemInfos();
    }
}
