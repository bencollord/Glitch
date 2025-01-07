namespace Glitch.Glob.Internal.Ast.Tokens
{
    internal class StringWildcard : GlobToken
    {
        internal static readonly StringWildcard Value = new StringWildcard();

        private StringWildcard()
            : base(GlobTokenType.StringWildcard) { }

        internal override string Text => "*";
    }
}
