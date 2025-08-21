namespace Glitch.Functional
{
    public readonly record struct Success<T>(T Value) : IEquatable<Success<T>>
    {
        public Success<TResult> Map<TResult>(Func<T, TResult> map) => new(map(Value));

        public Success<TResult> Apply<TResult>(Success<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        public Success<TResult> AndThen<TResult>(Func<T, Success<TResult>> bind)
            => bind(Value);

        public Success<TResult> AndThen<TElement, TResult>(Func<T, Success<TElement>> bind, Func<T, TElement, TResult> project)
            => new(project(Value, bind(Value).Value));

        public Success<TResult> Cast<TResult>()
            => new((TResult)(dynamic)Value!);

        public override string ToString() 
            => $"Okay({Value})";

        public static implicit operator Success<T>(T value) => new(value);

        public static implicit operator T(Success<T> success) => success.Value;
    }
}
