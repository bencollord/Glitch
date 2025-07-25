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

        public override ParseResult<TToken, TResult> AndThen<TResult>(Func<T, ParseResult<TToken, TResult>> bind) => bind(Value);

        public override ParseResult<TToken, T> OrElse(Func<ParseError<TToken, T>, ParseResult<TToken, T>> bind) => this;

        public override ParseResult<TToken, TResult> Map<TResult>(Func<T, TResult> map) => AndThen(v => new ParseSuccess<TToken, TResult>(map(v), Remaining));

        public override ParseResult<TToken, TResult> Cast<TResult>() => Map(DynamicCast<TResult>.From);

        public override TResult Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> ifOkay, Func<ParseError<TToken, T>, TResult> _) => ifOkay(this);

        public override string ToString() => $"Success: {Value}, Remaining: {Remaining}";

        public void Deconstruct(out T value, out TokenSequence<TToken> remaining)
        {
            value = Value;
            remaining = Remaining;
        }
    }
}
