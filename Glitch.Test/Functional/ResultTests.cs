using static Glitch.Functional.Results.Result;

namespace Glitch.Test.Functional
{
    public class ResultTests
    {
        [Fact]
        public void ZipWith_BothResultsOkay_ShouldApplyFunction()
        {
            // Arrange
            var left  = Okay(10);
            var right = Okay(20);

            // Act
            var result = left.Zip(right, (x, y) => x + y);

            // Assert
            Assert.True(result.IsOkay);
            Assert.Equal(30, result.UnwrapOrThrow());
        }

        [Fact]
        public void ZipWith_OneResultFailed_ShouldReturnErroredResult()
        {
            // Arrange
            var okay = Okay(10);
            var leftError = Fail<int>("Left failed");
            var rightError = Fail<int>("Right failed");

            // Act
            var leftResult = leftError.Zip(okay, (x, y) => x + y);
            var rightResult = okay.Zip(rightError, (x, y) => x + y);

            // Assert
            Assert.False(leftResult.IsOkay);
            Assert.False(rightResult.IsOkay);
            Assert.Equal("Left failed", leftResult.UnwrapError().Message);
            Assert.Equal("Right failed", rightResult.UnwrapError().Message);
        }
    }
}
