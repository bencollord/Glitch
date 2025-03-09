namespace Glitch.Functional
{
    public static class UnitExtensions
    {
        public static Option<Unit> IgnoreValue<T>(this Option<T> option) => option.Map(v => v.Ignore());
        public static Result<Unit> IgnoreValue<T>(this Result<T> result) => result.Map(v => v.Ignore());
        public static Fallible<Unit> IgnoreValue<T>(this Fallible<T> fallible) => fallible.Map(v => v.Ignore());
        public static Validation<Unit> IgnoreValue<T>(this Validation<T> validation) => validation.Map(v => v.Ignore());
    }
}
