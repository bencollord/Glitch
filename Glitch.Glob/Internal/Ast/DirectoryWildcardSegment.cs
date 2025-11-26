namespace Glitch.Glob.Internal.Ast;

internal class DirectoryWildcardSegment : Segment
{
    private Segment next;

    internal DirectoryWildcardSegment(Segment next)
    {
        ArgumentNullException.ThrowIfNull(next, nameof(next));
        this.next = next;
    }

    public override string ToString() => "**";

    internal override IEnumerable<FileSystemInfo> Expand(DirectoryInfo root)
    {
        foreach (var directory in root.EnumerateDirectories())
        {
            var nextMatches = next.Expand(directory);

            // If there are no matches for the next segment, recurse into this directory and try again.
            // TODO Max depth for security (if this wasn't just a side project).
            if (!nextMatches.Any())
            {
                nextMatches = Expand(directory);
            }

            foreach (var match in nextMatches)
            {
                yield return match;
            }
        }
    }
}
