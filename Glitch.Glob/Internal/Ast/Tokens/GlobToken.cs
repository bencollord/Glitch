namespace Glitch.Glob.Internal.Ast.Tokens
{
    internal abstract class GlobToken
    {
        protected GlobToken(GlobTokenType type)
        {
            Type = type;
        }

        internal abstract string Text { get; }

        internal GlobTokenType Type { get; }
    }
}
