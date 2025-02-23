namespace Glitch.Functional
{
    public static class Reader
    {
        public static Reader<TEnv, TValue> Lift<TEnv, TValue>(TValue value) => new(_ => value);
        public static Reader<TEnv, TValue> Lift<TEnv, TValue>(Func<TValue> thunk) => new(_ => thunk());
        public static Reader<TEnv, TValue> Lift<TEnv, TValue>(Func<TEnv, TValue> thunk) => new(thunk);
    }

    public class Reader<TEnv, TValue>
    {
        private readonly Func<TEnv, TValue> thunk;

        internal Reader(Func<TEnv, TValue> thunk)
        {
            this.thunk = thunk;
        }

        public Reader<TEnv1, TValue> With<TEnv1>(Func<TEnv1, TEnv> argMapper)
            => new(arg1 => thunk(argMapper(arg1)));

        public Reader<TEnv, TResult> Map<TResult>(Func<TValue, TResult> mapper)
            => new(arg => mapper(thunk(arg)));

        public Reader<TEnv, Func<T2, TResult>> PartialMap<T2, TResult>(Func<TValue, T2, TResult> mapper)
            => Map(mapper.Curry());

        public Reader<TEnv, TResult> AndThen<TResult>(Func<TValue, Reader<TEnv, TResult>> bind)
            => new(arg => bind(thunk(arg)).thunk(arg));

        public Reader<TEnv, TResult> AndThen<TElement, TResult>(Func<TValue, Reader<TEnv, TElement>> bind, Func<TValue, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public TValue Run(TEnv arg) => thunk(arg);
    }
}
