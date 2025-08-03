using Glitch.Functional.Parsing.Parsers;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, IEnumerable<T>> AtLeastOnce()
        {
            return from once in Map(ImmutableList.Create)
                   from tail in ZeroOrMoreTimes()
                   select once.Concat(tail);
        }

        public virtual Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes() => new RepeatParser<TToken, T>(this);

        public virtual Parser<TToken, IEnumerable<T>> Times(int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return AtLeastOnce()
                .Guard(items => items.Count() == count,
                       // TODO Extract expected tokens
                       // UNDONE 
                       items => $"Expected {count} times, found {items.Count()} times");
        }
    }
}
