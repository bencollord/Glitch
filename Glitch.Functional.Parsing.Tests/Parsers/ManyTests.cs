using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing.Tests.Parse;

public class ManyTests
{
    [Fact]
    public void ZeroOrMoreTimes_Succeeds_ReturnsItems()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void ZeroOrMoreTimes_Fails_SucceedsWithEmptyEnumerable()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Once_Succeeds_ReturnsSingletonCollection()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Once_SucceedsMoreThanOnce_SucceedsWithSingletonCollection_DoesNotConsumeFurther()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Once_Fails_ReturnsError()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void AtLeast_Fails_ReturnsError()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void AtLeast_SucceedsLessThanSpecifiedTimes_ReturnsError()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void AtLeast_SucceedsMoreThanCount_ReturnsAllMatches()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void AtMost_NoMatches_ReturnsEmptyCollection()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void AtMost_SucceedsLessThanCount_ReturnsAllMatches()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void AtMost_SucceedsMoreThanCount_Fails()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Times_MatchesExactlyCount_Succeeds()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Times_MatchesLessThanCount_Fails()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Times_MatchesMoreThanCount_Fails()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Until_StopSucceeds_ConsumesStop_ReturnsAllSuccesses()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void Until_StopNeverSucceeds_ContinuesToEndOfInput_ReturnsAllSuccesses()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }
}
