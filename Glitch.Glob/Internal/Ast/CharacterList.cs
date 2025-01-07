using System.Collections.Immutable;

namespace Glitch.Glob.Internal.Ast
{
    internal abstract class CharacterList : CharacterClass
    {
        protected abstract ImmutableHashSet<char> Characters { get; }

        internal static CharacterList Combine(IEnumerable<CharacterList> nodes)
        {
            ArgumentNullException.ThrowIfNull(nodes, nameof(nodes));
            return new CompositeCharacterListNode(nodes.ToImmutableList());
        }

        // TODO Support GlobOptions
        protected override bool IsMatch(char c) => Characters.Contains(c);

        private class CompositeCharacterListNode : CharacterList
        {
            internal CompositeCharacterListNode(ImmutableList<CharacterList> childeren)
            {
                var characters = ImmutableHashSet.CreateBuilder<char>();

                foreach (var child in childeren)
                {
                    characters.UnionWith(child.Characters);
                }

                Characters = characters.ToImmutable();
                RawText = childeren.Select(c => c.RawText).Join();
            }

            protected override ImmutableHashSet<char> Characters { get; }

            protected override string RawText { get; }
        }
    }
}
