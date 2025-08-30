using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, Unit> Skip() => Maybe().IgnoreResult();
    }
}