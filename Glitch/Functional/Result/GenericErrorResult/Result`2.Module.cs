namespace Glitch.Functional
{
    // TODO Incomplete
    public static partial class Result
    {
        public static bool IsOkay<TOkay, TError>(Result<TOkay, TError> result) => result.IsOkay;

        public static bool IsFail<TOkay, TError>(Result<TOkay, TError> result) => result.IsFail;

        public static Result<TOkay, TError> Guard<TOkay, TError>(bool condition, TOkay value, TError error)
            => condition ? new Okay<TOkay, TError>(value) : new Fail<TOkay, TError>(error);

        public static Result<TOkay, TError> Guard<TOkay, TError>(bool condition, TOkay value, Func<TOkay, TError> error)
            => condition ? new Okay<TOkay, TError>(value) : new Fail<TOkay, TError>(error(value));

        public static Result<TOkay, TError> Guard<TOkay, TError>(Func<TOkay, bool> predicate, TOkay value, TError error)
            => predicate(value) ? new Okay<TOkay, TError>(value) : new Fail<TOkay, TError>(error);
    }
}