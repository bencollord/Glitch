namespace Glitch.Text;

public static class FormatProviderExtensions
{
    public static T? GetFormat<T>(this IFormatProvider formatProvider)
        where T : class
    {
        return formatProvider.GetFormat(typeof(T)) as T;
    }
}
