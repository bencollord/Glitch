using Glitch.Functional;

namespace Glitch.Functional.Parsing.Input
{
    public record ByteSequence : ArrayTokenSequence<byte>
    {
        private byte[] source;

        public ByteSequence(byte[] source)
            : base(source)
        {
            this.source = source;
        }

        protected override string DisplayRemainder()
        {
            return string.Join(' ', source.Skip(Position + 1).Select(x => x.ToString("X")));
        }
    }
}
