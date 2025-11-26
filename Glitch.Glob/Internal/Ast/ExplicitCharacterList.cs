using System.Collections.Immutable;

namespace Glitch.Glob.Internal.Ast;

internal class ExplicitCharacterList : CharacterList
{
    private string rawText;

    internal ExplicitCharacterList(char[] characters)
    {
        Characters = characters.ToImmutableHashSet();
        rawText = new string(characters);
    }

    internal ExplicitCharacterList(string characters)
    {
        rawText = characters;
        Characters = characters.ToImmutableHashSet();
    }

    protected override ImmutableHashSet<char> Characters { get; }

    protected override string RawText => rawText;
}
