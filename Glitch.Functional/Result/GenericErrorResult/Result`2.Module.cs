using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Glitch.Functional
{
    // TODO Incomplete
    public static partial class Result
    {
        public static Result<TOkay, TError> Okay<TOkay, TError>(TOkay value) => new Success<TOkay, TError>(value);
        
        public static Result<TOkay, TError> Fail<TOkay, TError>(TError error) => new Failure<TOkay, TError>(error);

        public static bool IsOkay<TOkay, TError>(Result<TOkay, TError> result) => result.IsOkay;

        public static bool IsFail<TOkay, TError>(Result<TOkay, TError> result) => result.IsError;

        public static Result<TOkay, TError> Guard<TOkay, TError>(bool condition, TOkay value, TError error)
            => condition ? new Success<TOkay, TError>(value) : new Failure<TOkay, TError>(error);

        public static Result<TOkay, TError> Guard<TOkay, TError>(bool condition, TOkay value, Func<TOkay, TError> error)
            => condition ? new Success<TOkay, TError>(value) : new Failure<TOkay, TError>(error(value));

        public static Result<TOkay, TError> Guard<TOkay, TError>(Func<TOkay, bool> predicate, TOkay value, TError error)
            => predicate(value) ? new Success<TOkay, TError>(value) : new Failure<TOkay, TError>(error);
    }
}