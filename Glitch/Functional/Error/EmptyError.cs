namespace Glitch.Functional
{
    public class EmptyError : Error
    {
        public const int EmptyErrorCode = -0xDEAD;

        public static readonly EmptyError Value = new();

        private EmptyError() : base(EmptyErrorCode) { }

        public override string Message => "No error";

        public override Option<Error> Inner => None;

        public override Exception AsException() => throw new NullReferenceException("There's no actual error here");

        public override Error Combine(Error other) => other;

        public override IEnumerable<Error> Iterate()
        {
            yield break;
        }
    }
}
