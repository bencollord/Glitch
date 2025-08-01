using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Results
{
    public record ParseSuccess<TToken, T> : ParseResult<TToken, T>
    {
        public ParseSuccess(T value, TokenSequence<TToken> remaining) 
            : base(remaining)
        {
            Value = value;
        }

        public T Value { get; init; }

        public override bool WasSuccessful => true;

        public override ParseResult<TToken, TResult> Cast<TResult>() => Map(DynamicCast<TResult>.From);

        public override TResult Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> ifOkay, Func<ParseError<TToken, T>, TResult> _) => ifOkay(this);

        public override string ToString() => $"Success: {Value}, Remaining: {Remaining}";

        public void Deconstruct(out T value)
        {
            value = Value;
        }

        public void Deconstruct(out T value, out TokenSequence<TToken> remaining)
        {
            value = Value;
            remaining = Remaining;
        }
    }
}
