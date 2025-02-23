namespace Glitch.Functional
{
    // TODO Incomplete
    public static partial class Result
    {
        public static bool IsOkay<T>(Result<T> result) => result.IsOkay;

        public static bool IsFail<T>(Result<T> result) => result.IsFail;
    }
}
