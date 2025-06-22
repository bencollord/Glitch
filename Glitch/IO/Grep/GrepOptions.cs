namespace Glitch.Grep
{
    [Flags]
    public enum GrepOptions
    {
        None           = 0,
        FixedString    = 0x1,
        IgnoreCase     = 0x2,
        MatchWholeLine = 0x4,
        LocaleAware    = 0x8,
        Recursive      = 0x10
    }
}