using Glitch.Functional;
using System;
using System.Runtime.CompilerServices;
using static Glitch.Functional.FN;
using static Glitch.Functional.Result;

namespace Glitch.Test.Functional
{
    public class FallibleTests
    {
        [Fact]
        public void Map_ShouldNotRun_UntilRunMethodIsCalled()
        {
            // Arrange
            var item = Fallible<string>.Okay("Good afternoon");
            bool funcHasExecuted = false;
            var func = (string s) =>
            {
                funcHasExecuted = true;
                return s.ToUpper();
            };

            // Act
            var mapped = item.Map(func);
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
            var item = Fallible<string>.Okay("Test");
            var func = Func<string, string>(s => throw new Exception("Failure"));

            // Act
            var mapped = item.Map(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsFail);
            Assert.Equal("Failure", result.UnwrapError().Message);
        }

        [Fact]
        public void AndThen_ShouldNotRun_UntilRunMethodIsCalled()
        {
            // Arrange
            var item = Fallible<string>.Okay("Good afternoon");
            bool funcHasExecuted = false;
            var func = (string s) =>
            {
                funcHasExecuted = true;
                return Fallible.Okay(s.ToUpper());
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
            var item = Fallible<string>.Okay("Test");
            var func = Func<string, Fallible<string>>(s => throw new Exception("Failure"));

            // Act
            var mapped = item.AndThen(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsFail);
            Assert.Equal("Failure", result.UnwrapError().Message);
        }

        [Fact]
        public void Apply_ShouldApplyFunction_WhenBothAreSuccess()
        {
            // Arrange
            var item = Fallible.Okay("Test");
            var func = Fallible.Okay((string s) => s.ToUpper());

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsOkay);
            Assert.Equal("TEST", result.Unwrap());
        }

        [Fact]
        public void Apply_ShouldReturnFunctionError_WhenFunctionIsFail_AndValueIsSuccess()
        {
            // Arrange
            var item = Fallible.Okay("Test");
            var func = Fallible.Fail<Func<string, string>>("Function failed");

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsFail);
            Assert.Equal("Function failed", result.UnwrapError().Message);
        }

        [Fact]
        public void Apply_ShouldReturnValueError_WhenFunctionIsOkay_AndValueIsFail()
        {
            // Arrange
            var item = Fallible.Fail<string>("Value failed");
            var func = Fallible.Okay((string s) => s.ToUpper());

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsFail);
            Assert.Equal("Value failed", result.UnwrapError().Message);
        }

        [Fact]
        public void Apply_ShouldReturnValueError_WhenBothAreFailed()
        {
            // Arrange
            var item = Fallible.Fail<string>("Value failed");
            var func = Fallible.Fail<Func<string, string>>("Function failed");

            // Act
            var mapped = item.Apply(func);
            var result = mapped.Run();

            // Assert
            Assert.True(result.IsFail);
            Assert.Equal("Value failed", result.UnwrapError().Message);
        }

        [Fact]
        public void AllLazyMethods_ShouldReturnErrorResults_WhenFunctionThrows()
        {
            // Arrange
            var okayItem           = Fallible.Okay(10);
            var mapFunc            = (int _) => Throw<int>();
            var bindFunc           = (int _) => Throw<Fallible<int>>();
            var bindResultFunc     = (int _) => Throw<Result<int>>();
            var bindProjectionFunc = (int _, int _) => Throw<int>();
            var mapErrFunc         = (Error _) => Throw<Error>();
            var bindErrFunc        = (Error _) => Throw<Fallible<int>>();
            var filterFunc         = (int _) => Throw<bool>();
            var inlineError        = Error.New("Inline error");

            // Act
            Fallible<int> map            = okayItem.Map(mapFunc);
            Fallible<int> mapOr          = okayItem.MapOr(mapFunc, -1);
            Fallible<int> mapOrElse      = okayItem.MapOrElse(mapFunc, mapErrFunc);
            Fallible<int> mapOrError     = okayItem.MapOr(mapFunc, inlineError);
            Fallible<int> mapOrElseError = okayItem.MapOrElse(mapFunc, mapErrFunc);
            Fallible<int> andThen        = okayItem.AndThen(bindFunc);
            Fallible<int> andThen2       = okayItem.AndThen(bindFunc, bindProjectionFunc);
            Fallible<int> andThenResult  = okayItem.AndThen(bindResultFunc);
            Fallible<int> andThenResult2 = okayItem.AndThen(bindResultFunc, bindProjectionFunc);
            Fallible<int> andThenBiBind  = okayItem.AndThen(bindFunc, bindErrFunc);
            Fallible<int> filter         = okayItem.Filter(filterFunc);
            Fallible<int> zipWith        = okayItem.ZipWith(Fallible.Okay(1), bindProjectionFunc);

            // These two items will only run for faulted items
            var failItem           = Fallible.Fail<int>("Should be an error");
            Fallible<int> orElse   = failItem.OrElse(bindErrFunc);
            Fallible<int> mapError = failItem.MapError(mapErrFunc);

            var fallibleItems = new Dictionary<string, Fallible<int>>
            {
                [nameof(map)]            = map,
                [nameof(mapOr)]          = mapOr,
                [nameof(mapOrElse)]      = mapOrElse,
                [nameof(mapOrError)]     = mapOrError,
                [nameof(mapOrElseError)] = mapOrElseError,
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
                Assert.True(r.IsFail, $"{pair.Key} did not fail");
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

            // Wrap the above assertion into delegates that can be passed to Fallible's higher order functions.
            // The delegates are curried so we can pass the name of the case in to know which one failed.
            var mapFunc            = (string caseName) => (int _) => MarkCaseRun(caseName, resultValue);
            var bindFunc           = (string caseName) => (int _) => MarkCaseRun(caseName, Fallible.Okay(resultValue));
            var bindResultFunc     = (string caseName) => (int _) => MarkCaseRun(caseName, Okay(resultValue));
            var bindProjectionFunc = (string caseName) => (int _, int _) => MarkCaseRun(caseName, resultValue);
            var mapErrFunc         = (string caseName) => (Error _) => MarkCaseRun(caseName, errorValue);
            var mapErrToResultFunc = (string caseName) => (Error _) => MarkCaseRun(caseName, resultValue);
            var bindErrFunc        = (string caseName) => (Error _) => MarkCaseRun(caseName, Fallible.Okay(resultValue));
            var filterFunc         = (string caseName) => (int _) => MarkCaseRun(caseName, true);
            var okayItem           = Fallible.Okay(10);

            // Act
            Fallible<int> map            = okayItem.Map(mapFunc(nameof(map)));
            Fallible<int> mapOr          = okayItem.MapOr(mapFunc(nameof(mapOr)), -1);
            Fallible<int> mapOrError     = okayItem.MapOr(mapFunc(nameof(mapOrError)), errorValue);
            Fallible<int> andThen        = okayItem.AndThen(bindFunc(nameof(andThen)));
            Fallible<int> andThen2       = okayItem.AndThen(bindFunc(nameof(andThen2)), bindProjectionFunc(nameof(andThen2)));
            Fallible<int> andThenResult  = okayItem.AndThen(bindResultFunc(nameof(andThenResult)));
            Fallible<int> andThenResult2 = okayItem.AndThen(bindResultFunc(nameof(andThenResult2)), bindProjectionFunc(nameof(andThenResult2)));
            Fallible<int> filter         = okayItem.Filter(filterFunc(nameof(filter)));
            Fallible<int> zipWith        = okayItem.ZipWith(Fallible.Okay(1), bindProjectionFunc(nameof(zipWith)));

             // These two items will only run for faulted items
            var failItem           = Fallible.Fail<int>("Should be an error");
            Fallible<int> orElse   = failItem.OrElse(bindErrFunc(nameof(orElse)));
            Fallible<int> mapError = failItem.MapError(mapErrFunc(nameof(mapError)));

            // These items should be run for both Okay and Fail
            Fallible<int> mapOrElseOkay      = okayItem.MapOrElse(mapFunc(nameof(mapOrElseOkay)), _ => AssertNotCalled<int>());
            Fallible<int> mapOrElseFail      = failItem.MapOrElse(_ => AssertNotCalled<int>(), mapErrToResultFunc(nameof(mapOrElseFail)));
            Fallible<int> mapOrElseErrorOkay = okayItem.MapOrElse(mapFunc(nameof(mapOrElseErrorOkay)), _ => AssertNotCalled<Error>());
            Fallible<int> mapOrElseErrorFail = failItem.MapOrElse(_ => AssertNotCalled<int>(), mapErrFunc(nameof(mapOrElseErrorFail)));
            Fallible<int> andThenBiBindOkay  = okayItem.AndThen(bindFunc(nameof(andThenBiBindOkay)), err => AssertNotCalled<Fallible<int>>());
            Fallible<int> andThenBiBindFail  = failItem.AndThen(_ => AssertNotCalled<Fallible<int>>(), bindErrFunc(nameof(andThenBiBindFail)));
            
            var fallibles = new Dictionary<string, Fallible<int>>
            {
                [nameof(map)]                = map,
                [nameof(mapOr)]              = mapOr,
                [nameof(mapOrElseOkay)]      = mapOrElseOkay,
                [nameof(mapOrElseFail)]      = mapOrElseFail,
                [nameof(mapOrError)]         = mapOrError,
                [nameof(mapOrElseErrorOkay)] = mapOrElseErrorOkay,
                [nameof(mapOrElseErrorFail)] = mapOrElseErrorFail,
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
            var left  = Fallible<int>.Okay(10);
            var right = Fallible<int>.Okay(20);

            // Act
            var result = left.ZipWith(right, (x, y) => x + y).Run();

            // Assert
            Assert.True(result.IsOkay);
            Assert.Equal(30, result.Unwrap());
        }

        [Fact]
        public void ZipWith_OneResultFailed_ShouldReturnErroredResult()
        {
            // Arrange
            var okay = Fallible<int>.Okay(10);
            var leftError = Fallible<int>.Fail("Left failed");
            var rightError = Fallible<int>.Fail("Right failed");

            // Act
            var leftResult = leftError.ZipWith(okay, (x, y) => x + y).Run();
            var rightResult = okay.ZipWith(rightError, (x, y) => x + y).Run();

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
            var left = Fallible.Fail<int>("Left failed");
            var right = Fallible<int>.Fail("Right failed");

            // Act
            var result = left.ZipWith(right, (x, y) => x + y).Run();

            // Assert
            Assert.False(result.IsOkay);
            Assert.IsType<AggregateError>(result.UnwrapError());
            Assert.Contains(result.UnwrapError().Iterate(), e => e.Message == "Left failed");
            Assert.Contains(result.UnwrapError().Iterate(), e => e.Message == "Right failed");
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
