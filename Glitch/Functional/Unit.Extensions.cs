namespace Glitch.Functional
{
    public static class UnitExtensions
    {
        public static Option<Terminal> AsTerminal<T>(this Option<T> option) => option.Map(v => v.Ignore());
        public static Result<Terminal> AsTerminal<T>(this Result<T> result) => result.Map(v => v.Ignore());
        public static Fallible<Terminal> AsTerminal<T>(this Fallible<T> fallible) => fallible.Map(v => v.Ignore());
        public static Validation<Terminal> AsTerminal<T>(this Validation<T> validation) => validation.Map(v => v.Ignore());
    }
}
