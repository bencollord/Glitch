using Glitch.Functional;
using Glitch.Functional.Results;
using System;
using System.Runtime.CompilerServices;
using static Glitch.Functional.Results.Result;

namespace Glitch.Test.Functional
{
    public class TraverseTests
    {
        [Fact]
        public void Result_IfOneFails_AllFail()
        {
            // Arrange
            var successfulResults = Enumerable.Select(Sequence.Range(1, 10)
, Okay)
                .ToList();

            var failedResult = Fail<int>("Bad result");

            // Act
            successfulResults.Add(failedResult);

            Result<IEnumerable<int>> result = successfulResults.Traverse();

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Bad result", result.UnwrapError().Message);
        }
    }
}
