namespace Glitch.Functional
{
    public class EmptyError : Error
    {

        public static readonly EmptyError Value = new();

        private EmptyError() : base(ErrorCodes.None) { }

        public override string Message => "No error";

        public override Option<Error> Inner => FN.None;

        public override Exception AsException() => throw new BottomException("Error is empty");

        public override Error Combine(Error other) => other;

        public override IEnumerable<Error> Iterate()
        {
            yield break;
        }
    }
}
