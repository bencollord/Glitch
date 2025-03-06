using Glitch.Functional;
using System;
using System.Runtime.CompilerServices;
using static Glitch.Functional.FN;
using static Glitch.Functional.Result;

namespace Glitch.Test.Functional
{
    public class TraverseTests
    {
        [Fact]
        public void Result_IfOneFails_AllFail()
        {
            // Arrange
            var successfulResults = Range(1, 10)
                .Select(Okay)
                .ToList();

            var failedResult = Fail<int>("Bad result");

            // Act
            successfulResults.Add(failedResult);

            Result<IEnumerable<int>> result = successfulResults.Traverse();

            // Assert
            Assert.True(result.IsFail);
            Assert.Equal("Bad result", result.UnwrapError().Message);
        }
    }
}
