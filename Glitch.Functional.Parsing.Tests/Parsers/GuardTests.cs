using FluentAssertions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using Glitch.Functional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Glitch.Functional.Parsing.Tests.Parsers;

using static Parse;

public class GuardTests
{
    [Fact]
    public void Guard_PredicateMatches_Succeeds()
    {
        // Arrange
        var parser = Return(4).Guard(x => x < 5, "Invalid number");

        // Act
        var result = parser.Execute("Anything");

        // Assert
        result.IsOkay(out var val).Should().BeTrue();
        val.Should().Be(4);
    }

    [Fact]
    public void Guard_PredicateDoesNotMatch_Fails()
    {
        // Arrange
        var parser = Return(6).Guard(x => x < 5, "Invalid number");

        // Act
        var result = parser.Execute("Anything");

        // Assert
        result.IsFail(out var error).Should().BeTrue();
        error!.Message.Should().Be("Invalid number");
    }

    [Fact]
    public void Guard_PredicateDoesNotMatch_NoError_ReturnsEmptyError()
    {
        // Arrange
        var parser = Return(6).Guard(x => x < 5);

        // Act
        var result = parser.Execute("Anything");

        // Assert
        result.IsFail(out var error).Should().BeTrue();
        error!.Should().Be(ParseError.Empty);
    }
}
