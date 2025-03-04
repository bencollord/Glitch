namespace Glitch.Functional
{
    /// <summary>
    /// Static methods for <see cref="Option{T}"/>, mostly to simplify
    /// syntax when passing higher order functions.
    /// </summary>
    public static partial class Option
    {
        public static bool IsSome<T>(Option<T> option) => option.IsSome;

        public static bool IsNone<T>(Option<T> option) => option.IsNone;

        public static Option<T> Some<T>(T value) => Option<T>.Some(value);

        public static Option<T> None<T>() => Option<T>.None;

        public static Option<T> Maybe<T>(T? value) => Option<T>.Maybe(value);
    }
}
