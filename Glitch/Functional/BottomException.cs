using System.Runtime.Serialization;

namespace Glitch.Functional
{
    /// <summary>
    /// Exception that is thrown when code attempts to use the value of a 
    /// result or error type that is in an "empty" or "bottom" state.
    /// 
    /// This is effectively the exception version of <see cref="Error.Empty"/>, and is in
    /// fact the exception produced from that error's <see cref="Error.AsException()"/> method.
    /// If you see this exception, you most likely either tried to unwrap an empty <see cref="Option{T}"/>
    /// or tried to throw the error from a <see cref="Result{T}"/> that was filtered without mapping the error.
    /// </summary>
    /// <remarks>
    /// Certain types 
    /// </remarks>
    public class BottomException : Exception
    {
        private const string DefaultMessage = "Result or is in an empty, or 'bottom' state. " +
                                              "If you're seeing this message, ensure that you " + 
                                              "are not filtering a Result without providing " +
                                              "an explicit error.";

        public BottomException()
            : this(DefaultMessage) { }

        public BottomException(string? message) 
            : base(message)
        {
        }

        public BottomException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
