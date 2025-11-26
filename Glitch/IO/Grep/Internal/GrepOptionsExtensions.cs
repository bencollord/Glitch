namespace Glitch.Grep.Internal;

internal static class GrepOptionsExtensions
{
    internal static GrepOptions AddFlag(this GrepOptions flags, GrepOptions value)
    {
        return flags | value;
    }

    internal static GrepOptions RemoveFlag(this GrepOptions flags, GrepOptions value)
    {
        return flags & ~value;
    }
}
