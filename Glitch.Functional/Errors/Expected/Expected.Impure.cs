using Glitch.Functional.Core;

namespace Glitch.Functional.Errors
{
    public static partial class ExpectedExtensions
    {
        public static Expected<T> IfOkay<T>(this Expected<T> source, Action<T> action) =>
            source.Match(action, Nop).Return(source);

        public static Expected<T> IfOkay<T>(this Expected<T> source, Func<T, Unit> action) =>
            source.IfOkay(action.ReturnVoid());

        public static Expected<T> IfFail<T>(this Expected<T> source, Action<Error> action) =>
            source.Match(Nop, action).Return(source);

        public static Expected<T> IfFail<T>(this Expected<T> source, Func<Error, Unit> action) =>
            source.IfFail(action.ReturnVoid());

        // Alias for IfOkay
        public static Expected<T> Do<T>(this Expected<T> source, Action<T> action) =>
            source.Match(action, Nop).Return(source);

        public static Expected<T> Do<T>(this Expected<T> source, Func<T, Unit> action) =>
            source.IfOkay(action.ReturnVoid());

        public static Unit Match<T>(this Expected<T> source, Action<T> okay, Action<Error> fail) =>
            source.Match(okay.Return(), fail.Return());
    }
}
