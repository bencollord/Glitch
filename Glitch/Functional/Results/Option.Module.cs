namespace Glitch.Functional.Results
{
    /// <summary>
    /// Static methods for <see cref="Option{T}"/>, mostly to simplify
    /// syntax when passing higher order functions.
    /// </summary>
    public static partial class Option
    {
        public static OptionNone None => OptionNone.Value;

        public static bool IsSome<T>(Option<T> option) => option.IsSome;
        public static bool IsNone<T>(Option<T> option) => option.IsNone;

        public static Option<T> Filter<T>(T? value, Func<T, bool> predicate) => Maybe(value).Where(predicate);
        public static Option<T> Filter<T>(T? value, Func<T, bool> predicate) where T : struct => Maybe(value).Where(predicate);

        public static Option<T> Some<T>(T value) => Option<T>.Some(value);

        public static Option<T> Maybe<T>(T? value) => Option<T>.Maybe(value);
        public static Option<T> Maybe<T>(T? value) where T : struct
            => value.HasValue
             ? Some(value.Value)
             : None;
    }
}
