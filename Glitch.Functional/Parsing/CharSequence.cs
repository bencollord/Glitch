using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Functional.Parsing
{
    internal record CharSequence : TokenSequence<char>
    {
        private string sourceText;
        private int cursor;

        internal CharSequence(string sourceText)
        {
            this.sourceText = sourceText;
            cursor = 0;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override char Current => !IsEnd ? sourceText[cursor] : '\0';

        public override bool IsEnd => cursor >= sourceText.Length;

        public override TokenSequence<char> Advance()
        {
            return !IsEnd ? this with { cursor = cursor + 1 } : this;
        }

        public override TokenSequence<char> Advance(int count)
        {
            var nextPosition = cursor + count;

            return this with { cursor = Math.Min(nextPosition, sourceText.Length) };
        }

        public static implicit operator CharSequence(string text) => new(text);
    }
}
