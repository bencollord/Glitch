using Glitch.Functional.Core;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Parser<TToken, TResult> Select<TResult>(Func<T, TResult> selector)
        => new MapParser<TToken, T, TResult>(this, selector);

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Parser<TToken, TResult> Cast<TResult>() => Select(DynamicCast<TResult>.From);

    public virtual Parser<TToken, TResult> Return<TResult>(TResult value) => Then(Parser<TToken, TResult>.Return(value));

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Parser<TToken, Unit> IgnoreResult()
        => Select(_ => Unit.Value);

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Parser<TToken, TResult> Apply<TResult>(Parser<TToken, Func<T, TResult>> apply)
        => Then(x => apply.Select(y => y(x)));
}

internal class MapParser<TToken, T, TResult> : Parser<TToken, TResult>
{
    private readonly Parser<TToken, T> source;
    private readonly Func<T, TResult> projection;

    internal MapParser(Parser<TToken, T> source, Func<T, TResult> projection)
    {
        this.source = source;
        this.projection = projection;
    }

    public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
    {
        return source.Execute(input).Select(projection);
    }
}