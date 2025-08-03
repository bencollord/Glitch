namespace Glitch.Functional.Parsing
{
    using static Parse;

    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, T> Before<TOther>(Parser<TToken, TOther> parser)
            => Then(parser, (me, _) => me);

        public virtual Parser<TToken, T> After<TOther>(Parser<TToken, TOther> parser)
            => parser.Then(this, (_, me) => me);
    }
}
