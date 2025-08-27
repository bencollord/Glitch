using Glitch.Functional.Parsing;
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
                               .SeparatedBy(Literal(", "))
                               .AtLeastOnce();

            // Act
            var result = parser.Parse(csv);

            // Assert
            Assert.True(result.SequenceEqual(csv.Split(separator)));
        }
    }
}
