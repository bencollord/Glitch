namespace Glitch.Functional.Results
{
    public static class ErrorCodes
    {
        public const int None               = -0xBC00000;
        public const int Aggregate          = -0xBC00001;
        public const int NoElements         = -0xBC00002;
        public const int MoreThanOneElement = -0xBC00003;
        public const int ParseError         = -0xBC00004;
        public const int Unexpected         = -0xBC00005;
    }

    public static class Errors
    {
        public static readonly Error NoElements = Error.New(ErrorCodes.NoElements, "No elements found");

        public static readonly Error MoreThanOneElement = Error.New(ErrorCodes.MoreThanOneElement, "More than one element found");

        public static Error Unexpected<E>(E value) => Error.New(ErrorCodes.Unexpected, $"Unexpected {value}");
    }
}
