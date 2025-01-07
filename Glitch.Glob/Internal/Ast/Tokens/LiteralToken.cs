namespace Glitch.Glob.Internal.Ast.Tokens
{
    internal class LiteralToken : GlobToken
    {
        internal LiteralToken(string text)
            : base(GlobTokenType.Literal)
        {
            Text = text;
        }

        internal override string Text { get; }
    }
}
