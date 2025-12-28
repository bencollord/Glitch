using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, TResult> Select<TResult>(Func<T, TResult> selector) => new MapParser<TToken, T, TResult>(source, selector);

        public IParser<TToken, TResult> SelectMany<TElement, TResult>(Func<T, IParser<TToken, TElement>> bind, Func<T, TElement, TResult> project) => source.Then(bind, project);

        public IParser<TToken, T> Where(Func<T, bool> predicate) => source.Guard(predicate, ParseError.Empty);

        // UNDONE
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Parser<TToken, TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

        //public virtual Parser<TToken, TResult> Return<TResult>(TResult value) => Then(Parser<TToken, TResult>.Return(value));

        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public virtual Parser<TToken, Unit> IgnoreResult()
        //    => Select(_ => Unit.Value);
    }
}