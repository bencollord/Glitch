namespace Glitch.Functional.Results
{
    public record EmptyError : Error
    {

        public static readonly EmptyError Value = new();

        private EmptyError() : base(ErrorCodes.None, "No error") { }

        public override Option<Error> Inner => Option.None;

        public override Exception AsException() => throw new BottomException("Error is empty");

        public override Error Add(Error other) => other;

        public override IEnumerable<Error> Iterate()
        {
            yield break;
        }
    }
}
