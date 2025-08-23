namespace Glitch.Functional
{
    public readonly record struct Identity<T>(T Value) : IEquatable<Identity<T>>
    {
        public Identity<TResult> Map<TResult>(Func<T, TResult> map) => new(map(Value));

        public Identity<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        public Identity<TResult> Apply<TResult>(Identity<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        public Identity<TResult> AndThen<TResult>(Func<T, Identity<TResult>> bind)
            => bind(Value);

        public Identity<TResult> AndThen<TElement, TResult>(Func<T, Identity<TElement>> bind, Func<T, TElement, TResult> project)
            => new(project(Value, bind(Value).Value));

        public Identity<(T, TOther)> Zip<TOther>(Identity<TOther> other)
            => Zip(other, (x, y) => (x, y));

        public Identity<TResult> Zip<TOther, TResult>(Identity<TOther> other, Func<T, TOther, TResult> zipper)
            => new(zipper(Value, other.Value));

        public Identity<TResult> Cast<TResult>()
            => new((TResult)(dynamic)Value!);

        public T Unwrap() => Value;

        public IEnumerable<T> Iterate()
        {
            yield return Value;
        }

        public Result<T> Okay() => new Result.Success<T>(Value);

        public Option<T> Maybe() => Option<T>.Maybe(Value);

        public Option<T> Filter(Func<T, bool> predicate) => predicate(Value) ? Some(Value) : None;

        public Effect<TResult> Try<TResult>(Func<T, TResult> func) => Effect.Return(Value).Select(func);

        public override string ToString() 
            => $"Id({Value})";

        public static implicit operator Option<T>(Identity<T> identity) => identity.Maybe();

        public static implicit operator Result<T>(Identity<T> identity) => identity.Okay();

        public static implicit operator Effect<T>(Identity<T> identity) => identity.Try(i => i);

        public static implicit operator Sequence<T>(Identity<T> identity) => identity.Iterate().AsSequence();

        public static implicit operator Identity<T>(T value) => new(value);

        public static implicit operator T(Identity<T> id) => id.Value;
    }
}
