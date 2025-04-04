using System.Diagnostics;

namespace Glitch.Diagnostics
{
    internal static class GlitchDebug
    {
        internal static T? Fail<T>(string? message) => Fail(default(T), message);

        internal static T Fail<T>(T dummyValue, string? message)
        {
            Debug.Fail(message);
            return dummyValue;
        }
    }
}
