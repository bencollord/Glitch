namespace Glitch.Glob.Internal.Ast;

internal class StringWildcard : GlobNode
{
    internal const char Token = '*';

    internal static readonly StringWildcard MatchAll = new StringWildcard(NullNextNode.Value);

    private GlobNode next;

    private StringWildcard(GlobNode next)
    {
        ArgumentNullException.ThrowIfNull(next, nameof(next));
        this.next = next;
    }

    internal static StringWildcard MatchUntil(GlobNode next) => new StringWildcard(next);

    public override string ToString() => Token.ToString();

    internal override GlobMatch Match(string input, int pos)
    {
        // TODO Benchmark this
        for (int i = pos; i < input.Length; i++)
        {
            var match = next.Match(input, i);

            // Scan for next node's match. If found, the total match
            // length is the match plus the total scanned characters.
            if (match.IsSuccess)
            {
                return GlobMatch.Success(match.Length + i);
            }
        }

        return GlobMatch.Failure;
    }

    private class NullNextNode : GlobNode
    {
        internal static readonly NullNextNode Value = new NullNextNode();

        public override string ToString() => string.Empty;

        // If there is no next node, match immediately and return the length of the entire string.
        internal override GlobMatch Match(string input, int pos)
        {
            return GlobMatch.Success(input.Length - pos);
        }
    }
}
