using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        public Parser<TToken, IEnumerable<T>> AtLeastOnce()
        {
            return from once in Map(Sequence.Single)
                   from tail in ZeroOrMoreTimes()
                   select Enumerable.Concat(once, tail);
        }

        public Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes()
        {
            return new(input =>
            {
                var remaining = input;
                var items = new List<T>();

                while (!remaining.IsEnd)
                {
                    var result = parser(remaining);

                    if (!result.WasSuccessful)
                    {
                        break;
                    }

                    items.Add((T)result);
                    remaining = result.Remaining;
                }

                return ParseResult.Okay(items.AsEnumerable(), remaining);
            });
        }

        public Parser<TToken, IEnumerable<T>> Times(int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return AtLeastOnce()
                .Guard(items => items.Count() == count, 
                       // TODO Extract expected tokens
                       // UNDONE 
                       items => new Expectation<TToken>($"Expected {count} times, found {items.Count()} times"));

        }
    }
}
