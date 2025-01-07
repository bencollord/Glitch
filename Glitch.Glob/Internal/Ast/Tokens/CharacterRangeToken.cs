namespace Glitch.Glob.Internal.Ast.Tokens
{
    internal class CharacterRangeToken : GlobToken
    {
        private char start;
        private char end;

        internal CharacterRangeToken(char start, char end)
            : base(GlobTokenType.CharacterRange)
        {
            this.start = start;
            this.end = end;
        }

        internal override string Text => $"{start}-{end}";
    }
}
