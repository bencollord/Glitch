namespace Glitch.Glob.Internal.Ast;

internal class CharacterWildcard : GlobNode
{
    internal const char Token = '?';

    internal static readonly CharacterWildcard Value = new CharacterWildcard();

    private CharacterWildcard() { }

    public override string ToString() => Token.ToString();

    internal override GlobMatch Match(string input, int pos) => GlobMatch.Success(1);
}
