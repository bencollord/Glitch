namespace Glitch.Functional
{
    public readonly record struct Success<T>(T Value) : IEquatable<Success<T>>
    {
        public Success<TResult> Map<TResult>(Func<T, TResult> map) => new(map(Value));

        public Success<TResult> AndThen<TResult>(Func<T, Success<TResult>> bind)
            => bind(Value);

        public Success<TResult> AndThen<TElement, TResult>(Func<T, Success<TElement>> bind, Func<T, TElement, TResult> project)
            => new(project(Value, bind(Value).Value));

        public Option<TResult> AndThen<TResult>(Func<T, Option<TResult>> bind)
            => bind(Value);

        public Option<TResult> AndThen<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => Some(Value).AndThen(bind, project);

        public Result<TResult> AndThen<TResult>(Func<T, Result<TResult>> bind)
            => bind(Value);

        public Result<TResult> AndThen<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => Okay(Value).AndThen(bind, project);

        public Result<TResult, TError> AndThen<TResult, TError>(Func<T, Result<TResult, TError>> bind)
            => bind(Value);

        public Result<TResult, TError> AndThen<TError, TElement, TResult>(Func<T, Result<TElement, TError>> bind, Func<T, TElement, TResult> project)
            => Okay<T, TError>(Value).AndThen(bind, project);

        public Success<TResult> Cast<TResult>()
            => new((TResult)(dynamic)Value!);
        
        public override string ToString()
            => $"Okay({Value})";

        public static implicit operator Option<T>(Success<T> success) => Option<T>.Maybe(success.Value);

        public static implicit operator Success<T>(T value) => new(value);

        public static implicit operator T(Success<T> success) => success.Value;

        #region Linq
        public Success<TResult> Select<TResult>(Func<T, TResult> map) => new(map(Value));

        public Success<TResult> SelectMany<TResult>(Func<T, Success<TResult>> bind)
            => bind(Value);

        public Success<TResult> SelectMany<TElement, TResult>(Func<T, Success<TElement>> bind, Func<T, TElement, TResult> project)
            => new(project(Value, bind(Value).Value));

        public Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> bind)
            => bind(Value);

        public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => Some(Value).AndThen(bind, project);

        public Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> bind)
            => bind(Value);

        public Result<TResult> SelectMany<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => Okay(Value).AndThen(bind, project);

        public Result<TResult, TError> SelectMany<TResult, TError>(Func<T, Result<TResult, TError>> bind)
            => bind(Value);

        public Result<TResult, TError> SelectMany<TError, TElement, TResult>(Func<T, Result<TElement, TError>> bind, Func<T, TElement, TResult> project)
            => Okay<T, TError>(Value).AndThen(bind, project);
        #endregion
    }
}