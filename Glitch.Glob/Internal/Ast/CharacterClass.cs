namespace Glitch.Glob.Internal.Ast
{
    internal abstract class CharacterClass : GlobNode
    {
        internal static CharacterClass Negate(CharacterClass node)
        {
            if (node is NegatedCharacterClassNode negated)
            {
                return negated.Inner;
            }

            return new NegatedCharacterClassNode(node);
        }

        public override string ToString() => $"[{RawText}]";

        protected abstract string RawText { get; }

        internal sealed override GlobMatch Match(string input, int pos)
            => IsMatch(input[pos]) ? GlobMatch.Success(1) : GlobMatch.Failure;

        protected abstract bool IsMatch(char c);

        private class NegatedCharacterClassNode : CharacterClass
        {
            internal NegatedCharacterClassNode(CharacterClass inner)
            {
                ArgumentNullException.ThrowIfNull(inner, nameof(inner));
                Inner = inner;
            }

            internal CharacterClass Inner { get; }

            public override string ToString() => $"[!{RawText}]";

            protected override string RawText => Inner.RawText;

            protected override bool IsMatch(char c) => !Inner.IsMatch(c);
        }
    }
}
