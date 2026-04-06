namespace Glitch.Functional;

public readonly record struct Right<TRight>(TRight Value)
{
    public OneOf<TLeft, TRight> AsOneOf<TLeft>() => new OneOf<TLeft, TRight>.Right(Value);
}