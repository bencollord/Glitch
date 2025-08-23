namespace Glitch.Functional.Attributes
{
    internal static class Warnings
    {
        internal const string UseSelectInsteadOfMap = "'Map' is obsolete will be removed in the future. Use Select, which better matches C#'s naming conventions";
        internal const string UseSelectErrorInsteadOfMapError = "'MapError' is obsolete will be removed in the future. Use SelectError, which has better symmetry with Select";
        internal const string UseWhereInsteadOfFilter = "'Filter' is obsolete will be removed in the future. Use Where, which better matches C#'s naming conventions";
    }
}
