using Glitch.Functional.Parsing;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using static Glitch.Functional.Option;
using static Glitch.Functional.Parsing.Parse;

namespace Glitch.Functional.Tests.Parser
{
    public class StringTests
    {
        [Fact]
        public void AnyChar_ZeroOrMoreTimes_ReturnsAndConsumesWholeString()
        {
            // Arrange
            var text = "Foo, Bar, Baz";
            var parser = AnyChar.ZeroOrMoreTimes().AsString();

            // Act
            var result = parser.Execute(text);

            // Assert
            var ok = Assert.IsType<ParseSuccess<char, string>>(result);

            Assert.Equal(text, ok.Value);
            Assert.True(result.Remaining.IsEnd);
        }
    }
}
