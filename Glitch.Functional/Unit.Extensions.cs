namespace Glitch.Functional
{
    public static class UnitExtensions
    {
        public static Option<Unit> AsTerminal<T>(this Option<T> option) => option.Map(v => v.Ignore());
        public static Result<Unit> AsTerminal<T>(this Result<T> result) => result.Map(v => v.Ignore());
        public static Fallible<Unit> AsTerminal<T>(this Fallible<T> fallible) => fallible.Map(v => v.Ignore());
        public static Validation<Unit> AsTerminal<T>(this Validation<T> validation) => validation.Map(v => v.Ignore());
    }
}
