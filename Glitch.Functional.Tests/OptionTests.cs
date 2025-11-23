using FluentAssertions;
using Glitch.Functional;
using Glitch.Functional.Core;
using Glitch.Functional.Extensions;

namespace Glitch.Test.Functional
{
    using static Option;

    public class OptionTests
    {
        [Fact]
        public void Unwrap_ShouldThrowOptionSpecificException_AndBypassExtensionMethodDefault()
        {
            // Arrange
            Option<int> option = None;

            // Act/Assert
            option.Invoking(opt => opt.Unwrap())
                  .Should().Throw<InvalidOperationException>()
                  .WithMessage("Attempted to unwrap an empty option");
        }

        [Fact]
        public void Traverse_ShouldProduceOptionOfList_WhenFunctionIsSome()
        {
            // Arrange
            var items = Enumerable.Range(0, 10);

            // Act
            var traverse = items.Traverse(Some);

            // Assert
            Assert.True(traverse.IsSome);
            Assert.True(traverse.Unwrap().SequenceEqual(items));
        }

        [Fact]
        public void Traverse_ShouldReturnNone_WhenAnyValueInCollectionIsNone()
        {
            // Arrange
            var someIfEven = (int x) => x % 2 == 0 ? Some(x) : None;
            var items = Enumerable.Range(0, 10);

            // Act
            var traverse = items.Traverse(someIfEven);

            // Act
            Assert.True(traverse.IsNone);
        }

        [Fact]
        public void Traverse_ListOfOptionsWithFuncToValue_IsEquivalentTo_TraverseListOfValuesWithFuncToOption()
        {
            // Arrange
            var isEven = (int x) => x % 2 == 0;
            var someIfEven = (int x) => isEven(x) ? Some(x) : None;
            var stringify = (int x) => x.ToString("C");
            var stringifyIfEven = someIfEven.Then(opt => opt.Select(stringify));
            var values = Enumerable.Range(0, 10);
            var options = Enumerable.Range(0, 10).Select(someIfEven);

            // Act
            var successfulValues = values
                .Where(isEven)
                .Traverse(stringifyIfEven);

            var successfulOptions = options
                .Where(o => o.IsSome)
                .Traverse(stringify);

            var failedValues = values.Traverse(stringifyIfEven);
            var failedOptions = options.Traverse(stringify);

            Assert.False(failedValues.IsSome);
            Assert.False(failedOptions.IsSome);
            Assert.True(successfulOptions.Zip(successfulValues, (o, v) => o.SequenceEqual(v)));
        }
    }
}
