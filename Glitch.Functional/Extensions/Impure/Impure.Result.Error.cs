namespace Glitch.Functional.Extensions.Impure;
using static FN;

public static partial class ImpureExtensions
{
    extension<T>(Result<T> self)
    {
        public Result<T> IfOkay(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Result<T> IfOkay(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Result<T> IfFail(Action action) =>
            self.Match(Nop, action).Return(self);

        public Result<T> IfFail(Action<Error> action) =>
            self.Match(Nop, action).Return(self);

        // Alias for IfOkay
        public Result<T> Do(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Result<T> Do(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Unit Match(Action<T> okay, Action fail) =>
            self.Match(okay.Return(), fail.Return());

        public Unit Match(Action<T> okay, Action<Error> fail) =>
            self.Match(okay.Return(), fail.Return());
    }
}
