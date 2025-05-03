namespace Glitch.Functional
{
    internal static class ErrorCodes
    {
        internal const int Empty                 = -0xBC00000;
        internal const int Aggregate             = -0xBC00001;
        internal const int NoElements            = -0xBC00002;
        internal const int MoreThanOneElement    = -0xBC00003;
    }

    internal static class Errors
    {
        internal static readonly Error NoElements = Error.New(ErrorCodes.NoElements, "Sequence contains no elements");
        internal static readonly Error MoreThanOneElement = Error.New(ErrorCodes.MoreThanOneElement, "Sequence more than one element");

        internal static NotSupportedException BadDiscriminatedUnion() => new("If you've reached this message, a match against a discriminated union failed. You should not be extending it from your own code");
    }
}
