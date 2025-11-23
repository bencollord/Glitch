namespace Glitch.Functional.Extensions.Impure;

using Glitch.Functional.Errors;
using static FN;

public static partial class ImpureExtensions
{
    extension<T>(Expected<T> self)
    {
        public Expected<T> IfOkay(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Expected<T> IfOkay(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Expected<T> IfFail(Action action) =>
            self.Match(Nop, action).Return(self);

        public Expected<T> IfFail(Action<Error> action) =>
            self.Match(Nop, action).Return(self);

        // Alias for IfOkay
        public Expected<T> Do(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Expected<T> Do(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Unit Match(Action<T> okay, Action fail) =>
            self.Match(okay.Return(), fail.Return());

        public Unit Match(Action<T> okay, Action<Error> fail) =>
            self.Match(okay.Return(), fail.Return());
    }
}
