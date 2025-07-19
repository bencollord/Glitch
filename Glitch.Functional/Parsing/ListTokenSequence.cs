using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Functional.Parsing
{
    internal record ListTokenSequence<TToken> : TokenSequence<TToken>
    {
        private ImmutableList<TToken> tokens;
        private int cursor;

        internal ListTokenSequence(IEnumerable<TToken> tokens)
        {
            this.tokens = tokens.ToImmutableList();
            cursor = 0;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override TToken Current => !IsEnd ? tokens[cursor] : default!; // Suppress null warnings. It's the caller's responsibility to check the IsEnd property.

        public override bool IsEnd => cursor >= tokens.Count;

        public override TokenSequence<TToken> Advance()
        {
            return !IsEnd ? this with { cursor = cursor + 1 } : this;
        }

        public override TokenSequence<TToken> Advance(int count)
        {
            var nextPosition = cursor + count;

            return this with { cursor = Math.Min(nextPosition, tokens.Count) };
        }
    }
}
