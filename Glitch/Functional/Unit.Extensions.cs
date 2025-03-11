namespace Glitch.Functional
{
    public static class UnitExtensions
    {
        public static Option<Terminal> IgnoreValue<T>(this Option<T> option) => option.Map(v => v.Ignore());
        public static Result<Terminal> IgnoreValue<T>(this Result<T> result) => result.Map(v => v.Ignore());
        public static Fallible<Terminal> IgnoreValue<T>(this Fallible<T> fallible) => fallible.Map(v => v.Ignore());
        public static Validation<Terminal> IgnoreValue<T>(this Validation<T> validation) => validation.Map(v => v.Ignore());
    }
}
