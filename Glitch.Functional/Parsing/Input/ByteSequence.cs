using Glitch.Functional;
using Glitch.Functional.Parsing;

namespace Glitch.Functional.Parsing.Input
{
    internal record ByteSequence : ArrayTokenSequence<byte>
    {
        private byte[] source;

        internal ByteSequence(byte[] source)
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
