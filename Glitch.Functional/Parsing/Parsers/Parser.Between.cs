namespace Glitch.Functional.Parsing
{
    using static Parse;

    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, T> Between<TSeparator>(TToken separator)
           => Between(separator, separator);

        public virtual Parser<TToken, T> Between<TStart, TStop>(TStart start, TStop stop)
            => Between(Token(start), Token(stop));

        public virtual Parser<TToken, T> Between<TSeparator>(Parser<TToken, TSeparator> separator)
            => Between(separator, separator);

        public virtual Parser<TToken, T> Between<TStart, TStop>(Parser<TToken, TStart> start, Parser<TToken, TStop> stop)
            => from s in start
               from x in this
               from e in stop
               select x;
    }
}
