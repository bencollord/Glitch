namespace Glitch.Functional.Parsing.Input
{
    internal record EmptyTokenSequence<TToken> : TokenSequence<TToken>
    {
        public static readonly EmptyTokenSequence<TToken> Singleton = new();

        private EmptyTokenSequence() { }

        public override TToken Current => default!;

        public override int Position => 0;

        public override bool IsEnd => true;

        public override TokenSequence<TToken> Advance() => this;

        public override TokenSequence<TToken> Advance(int count) => this;

        protected override string DisplayRemainder() => string.Empty;
    }
}
