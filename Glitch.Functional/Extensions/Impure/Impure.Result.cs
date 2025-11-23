namespace Glitch.Functional.Extensions.Impure;

using static FN;

public static partial class ImpureExtensions
{
    extension<T, E>(Result<T, E> self)
    {
        public Result<T, E> IfOkay(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Result<T, E> IfOkay(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Result<T, E> IfFail(Action action) =>
            self.Match(Nop, action).Return(self);

        public Result<T, E> IfFail(Action<E> action) =>
            self.Match(Nop, action).Return(self);

        // Alias for IfOkay
        public Result<T, E> Do(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Result<T, E> Do(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Unit Match(Action<T> okay, Action fail) =>
            self.Match(okay.Return(), fail.Return());

        public Unit Match(Action<T> okay, Action<E> fail) =>
            self.Match(okay.Return(), fail.Return());
    }
}
