namespace Glitch.Functional.Errors
{
    public partial record Expected<T>
    {
        public Expected<T> IfOkay(Action<T> action) =>
            Match(action, Nop).Return(this);

        public Expected<T> IfOkay(Func<T, Unit> action) =>
            IfOkay(action.ReturnVoid());

        public Expected<T> IfFail(Action<Error> action) =>
            Match(Nop, action).Return(this);

        public Expected<T> IfFail(Func<Error, Unit> action) =>
            IfFail(action.ReturnVoid());

        // Alias for IfOkay
        public Expected<T> Do(Action<T> action) =>
            Match(action, Nop).Return(this);

        public Expected<T> Do(Func<T, Unit> action) =>
            IfOkay(action.ReturnVoid());

        public Unit Match(Action<T> okay, Action<Error> fail) =>
            Match(okay.Return(), fail.Return());
    }
}
