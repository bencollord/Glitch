namespace Glitch.Functional
{
    public static class Reader
    {
        public static Reader<TArg, TValue> New<TArg, TValue>(TValue value) => new(_ => value);
        public static Reader<TArg, TValue> New<TArg, TValue>(Func<TValue> thunk) => new(_ => thunk());
        public static Reader<TArg, TValue> New<TArg, TValue>(Func<TArg, TValue> thunk) => new(thunk);
    }

    public class Reader<TArg, TValue>
    {
        private Func<TArg, TValue> thunk;

        internal Reader(Func<TArg, TValue> thunk)
        {
            this.thunk = thunk;
        }

        public Reader<TArg1, TValue> With<TArg1>(Func<TArg1, TArg> argMapper)
            => new(arg1 => thunk(argMapper(arg1)));

        public Reader<TArg, TResult> Map<TResult>(Func<TValue, TResult> mapper)
            => new(arg => mapper(thunk(arg)));

        public Reader<TArg, Func<T2, TResult>> PartialMap<T2, TResult>(Func<TValue, T2, TResult> mapper)
            => Map(mapper.Curry());

        public Reader<TArg, TResult> AndThen<TResult>(Func<TValue, Reader<TArg, TResult>> bind)
            => new(arg => bind(thunk(arg)).thunk(arg));

        public Reader<TArg, TResult> AndThen<TElement, TResult>(Func<TValue, Reader<TArg, TElement>> bind, Func<TValue, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public TValue Run(TArg arg) => thunk(arg);
    }
}
