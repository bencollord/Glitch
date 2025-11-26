namespace Glitch.Glob.Internal.Ast;

internal class Literal : GlobNode
{
    private string text;
    private StringComparison comparison;

    internal Literal(string text, GlobOptions options)
    {
        this.text = text;
        comparison = MapComparison(options);
    }

    public override string ToString() => text;

    internal override GlobMatch Match(string input, int pos)
    {
        string matchAgainst = input.Substring(pos);

        if (text.Equals(matchAgainst, comparison))
        {
            return GlobMatch.Success(matchAgainst.Length);
        }

        return GlobMatch.Failure;
    }
}
