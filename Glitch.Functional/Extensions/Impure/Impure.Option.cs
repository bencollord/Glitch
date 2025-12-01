namespace Glitch.Functional.Extensions.Impure;

using System.Diagnostics;
using static FN;

[DebuggerStepThrough]
public static partial class ImpureExtensions
{
    extension<T>(Option<T> self)
    {
        public Option<T> IfSome(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Option<T> IfSome(Func<T, Unit> action) =>
            self.IfSome(action.ReturnVoid());

        public Option<T> IfNone(Action action) =>
            self.Match(Nop, action).Return(self);

        public Option<T> IfNone(Func<Unit> action) =>
            self.IfNone(action.ReturnVoid());

        public Option<T> IfNone(Action<Unit> action) =>
            self.Match(Nop, action).Return(self);

        public Option<T> Do(Action<T> action) =>
            self.Match(action, Nop).Return(self);

        public Option<T> Do(Func<T, Unit> action) =>
            self.IfSome(action.ReturnVoid());

        public Unit Match(Action<T> some, Action none) =>
            self.Match(some.Return(), none.Return());

        public Unit Match(Action<T> some, Action<Unit> none) =>
            self.Match(some.Return(), none.Return());
    }
}
