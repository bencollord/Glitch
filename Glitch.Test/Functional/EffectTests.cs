using Glitch.Functional;
using Glitch.Functional.Results;
using System;
using System.Runtime.CompilerServices;
using static Glitch.Functional.FN;
using static Glitch.Functional.Results.Result;

namespace Glitch.Test.Functional
{
    public class EffectTests
    {
        [Fact]
        public void Map_ShouldNotRun_UntilRunMethodIsCalled()
        {
            // Arrange
            var item = Effect<string>.Return("Good afternoon");
            bool funcHasExecuted = false;
            var func = (string s) =>
            {
                funcHasExecuted = true;
                return s.ToUpper();
            };

            // Act
            var mapped = item.Select(func);
            var wasExecutedBeforeRun = funcHasExecuted;
            var result = mapped.Run();
            var wasExecutedAfterRun = funcHasExecuted;

            // Assert
            Assert.Equal("GOOD AFTERNOON", result.Unwrap());
            Assert.False(wasExecutedBeforeRun);
            Assert.True(wasExecutedAfterRun);
        }

        [Fact]
        public void Map_FuncThrowsException_ReturnsFailedResult()
        {
            // Arrange
            var item = Effect<string>.Return("Test");
            var func = Func<string, string>(s => throw new Exception("Failure"));

            // Act
            var mapped = item.Select(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Failure", result.UnwrapError().Message);
        }

        [Fact]
        public void AndThen_ShouldNotRun_UntilRunMethodIsCalled()
        {
            // Arrange
            var item = Effect<string>.Return("Good afternoon");
            bool funcHasExecuted = false;
            var func = (string s) =>
            {
                funcHasExecuted = true;
                return Effect.Return(s.ToUpper());
            };

            // Act
            var mapped = item.AndThen(func);
            var wasExecutedBeforeRun = funcHasExecuted;
            var result = mapped.Run();
            var wasExecutedAfterRun = funcHasExecuted;

            // Assert
            Assert.Equal("GOOD AFTERNOON", result.Unwrap());
            Assert.False(wasExecutedBeforeRun);
            Assert.True(wasExecutedAfterRun);
        }

        [Fact]
        public void AndThen_FuncThrowsException_ReturnsFailedResult()
        {
            // Arrange
            var item = Effect<string>.Return("Test");
            var func = Func<string, Effect<string>>(s => throw new Exception("Failure"));

            // Act
            var mapped = item.AndThen(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Failure", result.UnwrapError().Message);
        }

        [Fact]
        public void Apply_ShouldApplyFunction_WhenBothAreSuccess()
        {
            // Arrange
            var item = Effect.Return("Test");
            var func = Effect.Return((string s) => s.ToUpper());

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsOkay);
            Assert.Equal("TEST", result.Unwrap());
        }

        [Fact]
        public void Apply_ShouldReturnFunctionError_WhenFunctionIsError_AndValueIsSuccess()
        {
            // Arrange
            var item = Effect.Return("Test");
            var func = Effect.Fail<Func<string, string>>("Function failed");

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Function failed", result.UnwrapError().Message);
        }

        [Fact]
        public void Apply_ShouldReturnValueError_WhenFunctionIsOkay_AndValueIsError()
        {
            // Arrange
            var item = Effect.Fail<string>("Value failed");
            var func = Effect.Return((string s) => s.ToUpper());

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Value failed", result.UnwrapError().Message);
        }

        [Fact]
        public void Apply_ShouldReturnValueError_WhenBothAreFailed()
        {
            // Arrange
            var item = Effect.Fail<string>("Value failed");
            var func = Effect.Fail<Func<string, string>>("Function failed");

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Value failed", result.UnwrapError().Message);
        }

        [Fact]
        public void AllLazyMethods_ShouldReturnErrorResults_WhenFunctionThrows()
        {
            // Arrange
            var okayItem           = Effect.Return(10);
            var mapFunc            = (int _) => Throw<int>();
            var bindFunc           = (int _) => Throw<Effect<int>>();
            var bindResultFunc     = (int _) => Throw<Result<int>>();
            var bindProjectionFunc = (int _, int _) => Throw<int>();
            var mapErrFunc         = (Error _) => Throw<Error>();
            var bindErrFunc        = (Error _) => Throw<Effect<int>>();
            var filterFunc         = (int _) => Throw<bool>();
            var inlineError        = Error.New("Inline error");

            // Act
            Effect<int> map            = okayItem.Select(mapFunc);
            Effect<int> andThen        = okayItem.AndThen(bindFunc);
            Effect<int> andThen2       = okayItem.AndThen(bindFunc, bindProjectionFunc);
            Effect<int> andThenResult  = okayItem.AndThen(bindResultFunc);
            Effect<int> andThenResult2 = okayItem.AndThen(bindResultFunc, bindProjectionFunc);
            Effect<int> andThenBiBind  = okayItem.Choose(bindFunc, bindErrFunc);
            Effect<int> filter         = okayItem.Filter(filterFunc);
            Effect<int> zipWith        = okayItem.Zip(Effect.Return(1), bindProjectionFunc);

            // These two items will only run for faulted items
            var failItem           = Effect.Fail<int>("Should be an error");
            Effect<int> orElse   = failItem.OrElse(bindErrFunc);
            Effect<int> mapError = failItem.SelectError(mapErrFunc);

            var fallibleItems = new Dictionary<string, Effect<int>>
            {
                [nameof(map)]            = map,
                [nameof(mapError)]       = mapError,
                [nameof(andThen)]        = andThen,
                [nameof(andThen2)]       = andThen2,
                [nameof(andThenResult)]  = andThenResult,
                [nameof(andThenResult2)] = andThenResult2,
                [nameof(andThenBiBind)]  = andThenBiBind,
                [nameof(orElse)]         = orElse,
                [nameof(filter)]         = filter,
                [nameof(zipWith)]        = zipWith,
            };

            // Assert
            Assert.All(fallibleItems, pair =>
            {
                var r = pair.Value.Run();
                Assert.True(r.IsError, $"{pair.Key} did not fail");
                Assert.Equal("Failure", r.UnwrapError().Message);
            });
        }

        [Fact]
        public void AllLazyMethods_Okay_ShouldNotRun_UntilRunIsCalled()
        {
            // Arrange
            var runCases = new HashSet<string>();

            T MarkCaseRun<T>(string caseName, T value)
            {
                runCases.Add(caseName);
                return value;
            }

            int resultValue = 20;
            var errorValue  = Error.New("Default error");

            // Wrap the above assertion into delegates that can be passed to Effect's higher order functions.
            // The delegates are curried so we can pass the name of the case in to know which one failed.
            var mapFunc            = (string caseName) => (int _) => MarkCaseRun(caseName, resultValue);
            var bindFunc           = (string caseName) => (int _) => MarkCaseRun(caseName, Effect.Return(resultValue));
            var bindResultFunc     = (string caseName) => (int _) => MarkCaseRun(caseName, Result.Okay(resultValue));
            var bindProjectionFunc = (string caseName) => (int _, int _) => MarkCaseRun(caseName, resultValue);
            var mapErrFunc         = (string caseName) => (Error _) => MarkCaseRun(caseName, errorValue);
            var mapErrToResultFunc = (string caseName) => (Error _) => MarkCaseRun(caseName, resultValue);
            var bindErrFunc        = (string caseName) => (Error _) => MarkCaseRun(caseName, Effect.Return(resultValue));
            var filterFunc         = (string caseName) => (int _) => MarkCaseRun(caseName, true);
            var okayItem           = Effect.Return(10);

            // Act
            Effect<int> map            = okayItem.Select(mapFunc(nameof(map)));
            Effect<int> andThen        = okayItem.AndThen(bindFunc(nameof(andThen)));
            Effect<int> andThen2       = okayItem.AndThen(bindFunc(nameof(andThen2)), bindProjectionFunc(nameof(andThen2)));
            Effect<int> andThenResult  = okayItem.AndThen(bindResultFunc(nameof(andThenResult)));
            Effect<int> andThenResult2 = okayItem.AndThen(bindResultFunc(nameof(andThenResult2)), bindProjectionFunc(nameof(andThenResult2)));
            Effect<int> filter         = okayItem.Filter(filterFunc(nameof(filter)));
            Effect<int> zipWith        = okayItem.Zip(Effect.Return(1), bindProjectionFunc(nameof(zipWith)));

             // These two items will only run for faulted items
            var failItem         = Effect.Fail<int>("Should be an error");
            Effect<int> orElse   = failItem.OrElse(bindErrFunc(nameof(orElse)));
            Effect<int> mapError = failItem.SelectError(mapErrFunc(nameof(mapError)));

            // These items should be run for both Okay and Fail
            Effect<int> andThenBiBindOkay  = okayItem.Choose(bindFunc(nameof(andThenBiBindOkay)), err => AssertNotCalled<Effect<int>>());
            Effect<int> andThenBiBindFail  = failItem.Choose(_ => AssertNotCalled<Effect<int>>(), bindErrFunc(nameof(andThenBiBindFail)));
            
            var fallibles = new Dictionary<string, Effect<int>>
            {
                [nameof(map)]                = map,
                [nameof(mapError)]           = mapError,
                [nameof(andThen)]            = andThen,
                [nameof(andThen2)]           = andThen2,
                [nameof(andThenResult)]      = andThenResult,
                [nameof(andThenResult2)]     = andThenResult2,
                [nameof(andThenBiBindOkay)]  = andThenBiBindOkay,
                [nameof(andThenBiBindFail)]  = andThenBiBindFail,
                [nameof(orElse)]             = orElse,
                [nameof(filter)]             = filter,
                [nameof(zipWith)]            = zipWith,
            };

            Assert.Empty(runCases);

            foreach (var (_, f) in fallibles)
            {
                _ = f.Run();
            }

            Assert.True(runCases.SetEquals(fallibles.Keys));
        }

        [Fact]
        public void ZipWith_BothOkay_ShouldApplyFunction()
        {
            // Arrange
            var left  = Effect<int>.Return(10);
            var right = Effect<int>.Return(20);

            // Act
            var result = left.Zip(right, (x, y) => x + y).Run();

            // Assert
            Assert.True(result.IsOkay);
            Assert.Equal(30, result.Unwrap());
        }

        [Fact]
        public void ZipWith_OneResultFailed_ShouldReturnErroredResult()
        {
            // Arrange
            var okay = Effect<int>.Return(10);
            var leftError = Effect<int>.Fail("Left failed");
            var rightError = Effect<int>.Fail("Right failed");

            // Act
            var leftResult = leftError.Zip(okay, (x, y) => x + y).Run();
            var rightResult = okay.Zip(rightError, (x, y) => x + y).Run();

            // Assert
            Assert.False(leftResult.IsOkay);
            Assert.False(rightResult.IsOkay);
            Assert.Equal("Left failed", leftResult.UnwrapError().Message);
            Assert.Equal("Right failed", rightResult.UnwrapError().Message);
        }

        // Utility methods
        private static T Throw<T>() => throw new InvalidOperationException("Failure");

        private static T AssertNotCalled<T>([CallerMemberName] string caller = "")
        {
            Assert.Fail($"Called function that should not be called. Caller: {caller}");
            return default;
        }
    }
}
