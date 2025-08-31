using Glitch.Functional.Results;

namespace Glitch.Functional.Parsing.Results
{
    using static Option;

    /// <summary>
    /// Factory methods for <see cref="Expectation{TToken}"/>.
    /// </summary>
    public static class Expectation
    {
        public static Expectation<TToken> Labeled<TToken>(string message) => new() { Label = message };
        public static Expectation<TToken> Unexpected<TToken>(TToken token) => new() { Unexpected = token };
        public static Expectation<TToken> Expected<TToken>(params IEnumerable<TToken> tokens) => new() { Expected = tokens };
    }

    public record Expectation<TToken>
    {
        public static readonly Expectation<TToken> None = new();

        public Option<string> Label { get; init; }

        public Option<TToken> Unexpected { get; init; }

        public IEnumerable<TToken> Expected { get; init; } = Enumerable.Empty<TToken>();

        public override string ToString()
        {
            var format = Curry<string, string, string>(string.Format);

            var expected = Label.Or(ExpectedMessage(Expected));

            var unexpected = Unexpected.AndThen(u => Maybe(u!.ToString())); // TODO Figure out why the compiler thinks that parameter will ever be null.

            var expectedButFound = from exp in expected
                                   from uxp in unexpected
                                   select $"Expected {exp}, but found {uxp}";

            return expectedButFound
                .Or(expected.Select(format("Expected {0}")))
                .Or(unexpected.Select(format("Unexpected {0}")))
                .IfNone(string.Empty);
        }

        private static Option<string> ExpectedMessage(IEnumerable<TToken> expected)
        {
            return from exp in Some(expected)
                   where exp.Any()
                   let msg = string.Join(", ", exp)
                   let idx = msg.LastIndexOf(',') + 1
                   select msg.Insert(idx, " or");
        }

        public static implicit operator Expectation<TToken>(string message) => new() { Label = message };
    }
}
