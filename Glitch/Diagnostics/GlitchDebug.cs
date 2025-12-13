using System.Diagnostics;

namespace Glitch.Diagnostics;

public static class GlitchDebug
{
    public static FluentAssertion Fail(string message)
    {
        Debug.Fail(message);
        return new FluentAssertion(message);
    }

    public readonly struct FluentAssertion
    {
        private readonly string? message;

        public FluentAssertion(string? message) => this.message = message;

#pragma warning disable CA1822 // Mark members as static. Type is meant to return an instance for fluent chaining.
        public T ThenReturn<T>(T value) => value;

        public void ThenThrow(Exception ex) => throw ex;

        public T ThenThrow<T>(Exception ex) => throw ex;

        public void ThenThrowUnreachable() => ThenThrow(new UnreachableException(message));

        public T ThenThrowUnreachable<T>() => ThenThrow<T>(new UnreachableException(message));

#pragma warning restore CA1822 // Mark members as static
    }
}
