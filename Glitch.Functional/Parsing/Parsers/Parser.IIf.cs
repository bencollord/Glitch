using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, TResult> IIf<TResult>(
            bool @if,
            Parser<TToken, TResult> then,
            Parser<TToken, TResult> @else)
            => IIf(@if, _ => then, _ => @else);

        public virtual Parser<TToken, TResult> IIf<TResult>(
            Func<T, bool> @if,
            Parser<TToken, TResult> then,
            Parser<TToken, TResult> @else)
            => IIf(@if, _ => then, _ => @else);


        public virtual Parser<TToken, TResult> IIf<TResult>(
            bool @if,
            Func<Parser<TToken, T>, Parser<TToken, TResult>> then,
            Func<Parser<TToken, T>, Parser<TToken, TResult>> @else) 
            => IIf(_ => @if, then, @else);

        public virtual Parser<TToken, TResult> IIf<TResult>(
            Func<T, bool> @if,
            Func<Parser<TToken, T>, Parser<TToken, TResult>> then,
            Func<Parser<TToken, T>, Parser<TToken, TResult>> @else)
        {
            return Then(v => @if(v) ? then(this) : @else(this));
        }
    }
}