using Glitch.Functional.Parsing;
using static Glitch.Functional.Parsing.Parse;

namespace Glitch.Test.Functional
{
    public class BeforeAfterTests
    {
        [Fact]
        public void Before_MatchComesBeforeToken_Succeeds()
        {
            // Arrange
            var csv = "Alpha,";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .Before(',');
            // Act
            var result = parser.Parse(csv);

            // Assert
            Assert.Equal("Alpha", result);
        }

        [Fact]
        public void Before_NoMatchForOther_Fails()
        {
            // Arrange
            var csv = "Alpha";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .Before(',');
            // Act
            var result = parser.Execute(csv);

            // Assert
            Assert.False(result.IsOkay);
            Assert.Contains(',', result.Expectation.Expected);
        }

        [Fact]
        public void After_MatchComesAfterToken_Succeeds()
        {
            // Arrange
            var csv = ",Bravo";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .After(',');
            // Act
            var result = parser.Parse(csv);

            // Assert
            Assert.Equal("Bravo", result);
        }

        [Fact]
        public void After_NoMatchForOther_Fails()
        {
            // Arrange
            var csv = "Bravo";

            var parser = Letter.AtLeastOnce()
                               .AsString()
                               .Before(',');
            // Act
            var result = parser.Execute(csv);

            // Assert
            Assert.False(result.IsOkay);
            Assert.Contains(',', result.Expectation.Expected);
        }
    }
}
