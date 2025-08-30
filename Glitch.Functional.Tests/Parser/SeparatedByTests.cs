using Glitch.Functional.Parsing;
using Glitch.Functional.Parsing.Results;
using static Glitch.Functional.Parsing.Parse;

namespace Glitch.Test.Functional
{
    public class SeparatedByTests
    {
        [Fact]
        public void SeparatedBy_WithSeparatedInput_Succeeds()
        {
            // Arrange
            var csv = "Alpha,Bravo,Charlie,Delta";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .SeparatedBy(',')
                               .AtLeastOnce();

            // Act
            var result = parser.Parse(csv);

            // Assert
            Assert.True(result.SequenceEqual(csv.Split(',')));
        }

        [Fact]
        public void SeparatedBy_WithStringSeparator_Succeeds()
        {
            // Arrange
            var separator = ", ";
            var csv = "Foo, Bar, Baz";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .SeparatedBy(Literal(separator))
                               .AtLeastOnce();

            // Act
            var result = parser.Parse(csv);

            // Assert
            Assert.True(result.SequenceEqual(csv.Split(separator)));
        }

        [Fact]
        public void SeparatedBy_WithSpecificNumberOfTimes_Succeeds_WhenCountMatches()
        {
            // Arrange
            var separator = ", ";
            var csv = "Foo, Bar, Baz";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .SeparatedBy(Literal(separator))
                               .Times(3);

            // Act
            var result = parser.Parse(csv).ToArray();

            // Assert
            Assert.Equal("Foo", result[0]);
            Assert.Equal("Bar", result[1]);
            Assert.Equal("Baz", result[2]);
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void SeparatedBy_WithSpecificNumberOfTimes_Fails_WithTooFewItems()
        {
            // Arrange
            var separator = ", ";
            var csv = "Foo, Bar";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .SeparatedBy(Literal(separator))
                               .Times(3);

            // Act
            var result = parser.Execute(csv);

            // Assert
            Assert.False(result.IsOkay);

            var error = Assert.IsType<ParseError<char, IEnumerable<string>>>(result);

            // TODO Better error messaging and test messages
        }

        [Fact]
        public void SeparatedBy_WithSpecificNumberOfTimes_Fails_WithTooManyItems()
        {
            // Arrange
            var separator = ", ";
            var csv = "Foo, Bar, Baz, Qux";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .SeparatedBy(Literal(separator))
                               .Times(3);

            // Act
            var result = parser.Execute(csv);

            // Assert
            Assert.False(result.IsOkay);

            var error = Assert.IsType<ParseError<char, IEnumerable<string>>>(result);

            // TODO Better error messaging and test messages
        }
    }
}
