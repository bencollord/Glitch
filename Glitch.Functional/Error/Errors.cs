namespace Glitch.Functional
{
    internal static class ErrorCodes
    {
        internal const int None              = -0xBC00000;
        internal const int Aggregate          = -0xBC00001;
        internal const int NoElements         = -0xBC00002;
        internal const int MoreThanOneElement = -0xBC00003;
        internal const int ParseError         = -0xBC00004;
    }

    internal static class Errors
    {
        internal static readonly Error NoElements = Error.New(ErrorCodes.NoElements, "Sequence contains no elements");

        internal static readonly Error MoreThanOneElement = Error.New(ErrorCodes.MoreThanOneElement, "Sequence more than one element");
    }
}
