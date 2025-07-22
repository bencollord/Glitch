using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    /// <summary>
    /// Factory methods for <see cref="Expectation{TToken}"/>.
    /// </summary>
    public static class Expectation
    {
        public static Expectation<TToken> Unexpected<TToken>(TToken token) => new(unexpected: token);
    }

    public record Expectation<TToken>
    {
        public static readonly Expectation<TToken> None = new(Option<string>.None, Option<TToken>.None, Option<IEnumerable<TToken>>.None);

        public Expectation(Option<string> label = default, Option<TToken> unexpected = default, Option<IEnumerable<TToken>> expected = default)
        {
            Label = label;
            Unexpected = unexpected;
            Expected = expected.Match(e => e.ToImmutableHashSet(), _ => []);
        }

        public Option<string> Label { get; init; }

        public Option<TToken> Unexpected { get; init; }

        public IImmutableSet<TToken> Expected { get; init; }

        public override string ToString()
        {
            var format = Curry<string, string, string>(string.Format);

            var expected = Label.Or(ExpectedMessage(Expected));

            var unexpected = Unexpected.AndThen(u => Maybe(u!.ToString())); // TODO Figure out why the compiler thinks that parameter will ever be null.

            var expectedButFound = from exp in expected
                                   from uxp in unexpected
                                   select $"Expected {exp}, but found {uxp}";

            return expectedButFound
                .Or(expected.Map(format("Expected {0}")))
                .Or(unexpected.Map(format("Unexpected {0}")))
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
    }
}
