using System.Runtime.Serialization;

namespace Glitch.Functional
{
    /// <summary>
    /// Exception that is thrown when a match on a type 
    /// intended to act as a discriminated union fails.
    /// </summary>
    /// <remarks>
    /// This exception is mainly intended to be thrown from the default case when pattern
    /// matching on types like <see cref="Result{TOkay, TError}"/> and <see cref="OneOf{TLeft, TRight}"/>.
    /// Semantically, they are discriminated unions and with their constructors being internal, nobody should be
    /// deriving from them, but the C# language has to treat the default case as if it can always happen.
    /// 
    /// Throwing this exception clears the compiler error, covers the case to make the match exhaustive,
    /// and in the unlikely event it actually is thrown, clearly signals an error on the developer's part.
    /// </remarks>
    public class BadDiscriminatedUnionException : Exception
    {
        private const string DefaultMessage = "If you've reached this message, a pattern match against a discriminated union reached the base case. " +
                                              "This error is only intended to handle the C# compiler wanting switch expression matches to be exhaustive, " +
                                              "so if you're seeing this message, you dun goofed";

        public BadDiscriminatedUnionException()
            : this(DefaultMessage) { }

        public BadDiscriminatedUnionException(object invalidValue, string message = DefaultMessage)
            : this(message)
        {
            InvalidValue = invalidValue;
        }

        public BadDiscriminatedUnionException(string? message) 
            : base(message)
        {
        }

        public BadDiscriminatedUnionException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public Option<object> InvalidValue { get; }

        public override string Message => InvalidValue.Match(
                                              v => $"{base.Message} Value: {v}",
                                              _ => base.Message
                                          );
    }
}
