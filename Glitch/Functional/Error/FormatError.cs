
namespace Glitch.Functional
{
    public class FormatError : ApplicationError
    {
        private const string MessageTemplate = "Invalid format: Could not parse '{0}' to '{1}'";

        public FormatError(string input, Type targetType) 
            : base(ErrorCodes.FormatError, string.Format(MessageTemplate, input, targetType))
        {
            Input = input;
            TargetType = targetType;
        }

        public string Input { get; }

        public Type TargetType { get; }

        public override Exception AsException() => new FormatException(Message);
    }
}
