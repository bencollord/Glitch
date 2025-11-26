namespace Glitch.Glob;

[Flags]
public enum GlobOptions
{
    None           = 0x0,
    IgnoreCase     = 0x1,
    LocaleAware    = 0x2,
    ExcludeDirectories = 0x4,
    ExcludeFiles   = 0x8
}
