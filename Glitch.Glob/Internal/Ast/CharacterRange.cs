using System.Collections.Immutable;

namespace Glitch.Glob.Internal.Ast
{
    internal class CharacterRange : CharacterList
    {
        private char start;
        private char end;

        internal CharacterRange(char start, char end)
        {
            this.start = start;
            this.end = end;
            Characters = Enumerable.Range(start, end - start + 1)
                .Select(c => (char)c)
                .ToImmutableHashSet();
        }

        protected override ImmutableHashSet<char> Characters { get; }

        protected override string RawText => $"{start}-{end}";
    }
}
