
namespace Glitch.Functional
{
    public class ParseError : ApplicationError
    {
        private const string MessageTemplate = "Could not parse '{0}' to '{1}'";

        public ParseError(string input, Type targetType) 
            : base(ErrorCodes.ParseError, string.Format(MessageTemplate, input, targetType))
        {
            Input = input;
            TargetType = targetType;
        }

        public string Input { get; }

        public Type TargetType { get; }

        public override Exception AsException() => new FormatException(Message);
    }
}
