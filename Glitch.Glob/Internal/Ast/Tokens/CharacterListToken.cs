namespace Glitch.Glob.Internal.Ast.Tokens
{
    internal class CharacterListToken : GlobToken
    {
        private char[] characters;

        internal CharacterListToken(char[] characters)
            : base(GlobTokenType.CharacterList)
        {
            this.characters = characters;
        }

        internal override string Text => new string(characters);
    }
}
