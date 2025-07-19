namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        public Parser<TToken, IEnumerable<T>> AtLeastOnce()
        {
            return new(input =>
            {
                var result = parser(input);
                var remaining = result.Remaining;

                if (!result.WasSuccessful)
                {
                    return result.Cast<IEnumerable<T>>();
                }

                var items = new List<T>();

                while (result.WasSuccessful)
                {
                    // Add previous result to the list
                    items.Add((T)result);
                    result = parser(remaining);
                    remaining = result.Remaining;

                    if (remaining.IsEnd)
                    {
                        // Add the current result to the list
                        items.Add((T)result);
                        break;
                    }
                }

                return ParseResult<TToken, IEnumerable<T>>.Okay([.. items], remaining);
            });
        }

        public Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes() => AtLeastOnce().Or(Parser<TToken>.Return(Enumerable.Empty<T>()));

        public Parser<TToken, IEnumerable<T>> Times(int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return AtLeastOnce()
                .Guard(items => items.Count() == count, 
                       items => $"Expected {string.Join(", ", items)} {count} times, found {items.Count()} times");

        }
    }
}
