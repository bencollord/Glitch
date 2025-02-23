using Glitch.Functional;
using static Glitch.Functional.FN;

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
            var result = left.ZipWith(right, (x, y) => x + y);

            // Assert
            Assert.True(result.IsOkay);
            Assert.Equal(30, result.Unwrap());
        }

        [Fact]
        public void ZipWith_OneResultFailed_ShouldReturnErroredResult()
        {
            // Arrange
            var okay = Okay(10);
            var leftError = Fail<int>("Left failed");
            var rightError = Fail<int>("Right failed");

            // Act
            var leftResult = leftError.ZipWith(okay, (x, y) => x + y);
            var rightResult = okay.ZipWith(rightError, (x, y) => x + y);

            // Assert
            Assert.False(leftResult.IsOkay);
            Assert.False(rightResult.IsOkay);
            Assert.Equal("Left failed", leftResult.UnwrapError().Message);
            Assert.Equal("Right failed", rightResult.UnwrapError().Message);
        }

        [Fact]
        public void ZipWith_BothResultsFailed_AggregateError()
        {
            // Arrange
            var left = Fail<int>("Left failed");
            var right = Fail<int>("Right failed");

            // Act
            var result = left.ZipWith(right, (x, y) => x + y);

            // Assert
            Assert.False(result.IsOkay);
            Assert.IsType<AggregateError>(result.UnwrapError());
            Assert.Contains(result.UnwrapError().Iterate(), e => e.Message == "Left failed");
            Assert.Contains(result.UnwrapError().Iterate(), e => e.Message == "Right failed");
        }
    }
}
