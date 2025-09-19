using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public Parser<TToken, T> Except<TOther>(Parser<TToken, TOther> other)
            => Then(other.Not());
    }
}