using Glitch.Functional;
using Glitch.Functional.Collections;

namespace Glitch.Test.Functional;

public class SequenceTests
{
    [Fact]
    public void IfEmpty_WithFunction_DoesNotRunFunction_IfSequenceIsNotEmpty()
    {
        // Arrange
        var seq = new Sequence<int>([1, 2, 3]);
        var func = () =>
        {
            Assert.Fail("Function was called");
            return 1;
        };

        // Act
        var res = seq.IfEmpty(func);

        // Assert
        Assert.True(res.SequenceEqual(seq));
    }

    [Fact]
    public void AdditionOperator_TwoSequences_Concatenates()
    {
        // Arrange
        var seqX = Sequence.Of(1, 2, 3);
        var seqY = Sequence.Of(4, 5, 6);
        var expected = Enumerable.Range(1, 6);

        // Act
        var result = seqX + seqY;

        // Assert
        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public void AdditionOperator_SingleItem_AsRhs_Appends()
    {
        // Arrange
        var seqX = Sequence.Of(1, 2, 3);
        var y = 4;
        var expected = Enumerable.Range(1, 4);

        // Act
        var result = seqX + y;

        // Assert
        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public void AdditionOperator_SingleItem_AsLhs_Prepends()
    {
        // Arrange
        var x = 1;
        var seqY = Sequence.Of(2, 3, 4);
        var expected = Enumerable.Range(1, 4);

        // Act
        var result = x + seqY;

        // Assert
        Assert.True(expected.SequenceEqual(result));
    }
}
