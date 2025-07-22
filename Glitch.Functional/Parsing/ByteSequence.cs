namespace Glitch.Functional.Parsing
{
    internal record ByteSequence : TokenSequence<byte>
    {
        private byte[] source;
        private int cursor;

        internal ByteSequence(byte[] source)
        {
            this.source = source;
            cursor = 0;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override byte Current => !IsEnd ? source[cursor] : byte.MinValue;

        public override bool IsEnd => cursor >= source.Length;

        public override TokenSequence<byte> Advance()
        {
            return !IsEnd ? this with { cursor = cursor + 1 } : this;
        }

        public override TokenSequence<byte> Advance(int count)
        {
            var nextPosition = cursor + count;

            return this with { cursor = Math.Min(nextPosition, source.Length) };
        }
    }
}
