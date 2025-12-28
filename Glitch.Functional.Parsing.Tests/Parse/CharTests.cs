using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing.Tests.Parse;

public class CharTests
{
    [Fact]
    public void AnyChar_Succeeds()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void LetterOrDigit_IsLetterOrDigit_Succeeds()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Fact]
    public void LetterOrDigit_NonLetterOrDigit_Fails()
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Theory]
    [InlineData(' ')]
    [InlineData('\t')]
    public void NonBreakingSpace_TabOrSpace_Succeeds(char character)
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }

    [Theory]
    [InlineData('\r')]
    [InlineData('\n')]
    public void NonBreakingSpace_LineBreak_Fails(char character)
    {
        // Arrange
        // Act
        // Assert
        throw new NotImplementedException();
    }
}
