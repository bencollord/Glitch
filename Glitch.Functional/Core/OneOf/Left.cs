namespace Glitch.Functional.Core;

public readonly record struct Left<TLeft>(TLeft Value)
{ 
    public OneOf<TLeft, TRight> AsOneOf<TRight>() => new OneOf<TLeft, TRight>.Left(Value);
}