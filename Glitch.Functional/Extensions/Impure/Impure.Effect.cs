namespace Glitch.Functional.Extensions.Impure;

using Glitch.Functional.Effects;
using Glitch.Functional.Errors;

public static partial class ImpureExtensions
{
    extension<T>(Effect<T> self)
    {
        public Effect<T> IfOkay(Action<T> action) =>
            self.Select(x => action.Return(x)(x));

        public Effect<T> IfOkay(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Effect<T> IfFail(Action action) =>
            self.IfFail(_ => action());

        public Effect<T> IfFail(Action<Error> action) =>
            self.SelectError(e => action.Return(e)(e));

        // Alias for IfOkay
        public Effect<T> Do(Action<T> action) =>
            self.IfOkay(action);

        public Effect<T> Do(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Effect<Unit> Match(Action<T> okay, Action fail) =>
            self.Match(okay.Return(), fail.Return());

        public Effect<Unit> Match(Action<T> okay, Action<Error> fail) =>
            self.Match(okay.Return(), fail.Return());
    }

    extension<TInput, T>(Effect<TInput, T> self)
    {
        public Effect<TInput, T> IfOkay(Action<T> action) =>
            self.Select(x => action.Return(x)(x));

        public Effect<TInput, T> IfOkay(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Effect<TInput, T> IfFail(Action action) =>
            self.IfFail(_ => action());

        public Effect<TInput, T> IfFail(Action<Error> action) =>
            self.SelectError(e => action.Return(e)(e));

        // Alias for IfOkay
        public Effect<TInput, T> Do(Action<T> action) =>
            self.IfOkay(action);

        public Effect<TInput, T> Do(Func<T, Unit> action) =>
            self.IfOkay(action.ReturnVoid());

        public Effect<TInput, Unit> Match(Action<T> okay, Action fail) =>
            self.Match(okay.Return(), fail.Return());

        public Effect<TInput, Unit> Match(Action<T> okay, Action<Error> fail) =>
            self.Match(okay.Return(), fail.Return());
    }
}
