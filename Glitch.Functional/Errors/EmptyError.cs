using Glitch.Functional.Core;

namespace Glitch.Functional.Errors
{
    public record EmptyError : Error
    {

        public static readonly EmptyError Value = new();

        private EmptyError() : base((int)GlobalErrorCode.None, "Empty error") { }

        public override Option<Error> Inner => Option.None;

        public override Exception AsException() => throw new InvalidOperationException("Error is empty");

        public override Error Add(Error other) => other;

        public override IEnumerable<Error> Iterate()
        {
            yield break;
        }
    }
}
