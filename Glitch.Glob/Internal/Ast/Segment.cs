namespace Glitch.Glob.Internal.Ast;

internal abstract class Segment
{
    public abstract override string ToString();

    internal abstract IEnumerable<FileSystemInfo> Expand(DirectoryInfo root);
}
