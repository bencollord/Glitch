namespace Glitch.Glob.Internal.Ast.Tokens
{
    internal class CharacterWildcard : GlobToken
    {
        internal static readonly CharacterWildcard Value = new CharacterWildcard();

        private CharacterWildcard()
            : base(GlobTokenType.CharacterWildcard) { }

        internal override string Text => "?";
    }
}
