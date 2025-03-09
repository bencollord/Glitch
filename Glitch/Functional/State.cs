namespace Glitch.Functional
{
    public static class State
    {
        public static State<TState, TState> Get<TState>() => new(s => (s, s));

        public static State<TState, TValue> Get<TState, TValue>(Func<TState, TValue> func) => new(s => (s, func(s)));

        public static State<TState, Unit> Put<TState>(TState state) => new(_ => (state, Unit.Value));

        public static State<TState, Unit> Modify<TState>(Func<TState, TState> func) => new(s => (func(s), Unit.Value));

        public static State<TState, TValue> Lift<TState, TValue>(TValue value) => new(s => (s, value));
        
        public static State<TState, TValue> Lift<TState, TValue>(Func<TValue> thunk) => new(s => (s, thunk()));
        
        public static State<TState, TValue> Lift<TState, TValue>(Func<TState, TValue> thunk) => new(s => (s, thunk(s)));
    }

    public static class State<TState>
    {
        public static State<TState, TState> Get() => new(s => (s, s));

        public static State<TState, TValue> Get<TValue>(Func<TState, TValue> func) => new(s => (s, func(s)));

        public static State<TState, Unit> Put(TState state) => new(_ => (state, Unit.Value));

        public static State<TState, Unit> Modify(Func<TState, TState> func) => new(s => (func(s), Unit.Value));

        public static State<TState, TValue> Lift<TValue>(TValue value) => new(s => (s, value));

        public static State<TState, TValue> Lift<TValue>(Func<TValue> thunk) => new(s => (s, thunk()));

        public static State<TState, TValue> Lift<TValue>(Func<TState, TValue> thunk) => new(s => (s, thunk(s)));
    }

    public record StateResult<TState, TValue>(TState State, TValue Value)
    {
        public static implicit operator StateResult<TState, TValue>((TState State, TValue Value) tuple)
            => new(tuple.State, tuple.Value);
    }

    public class State<TState, TValue>
    {
        private Func<TState, StateResult<TState, TValue>> thunk;

        internal State(Func<TState, StateResult<TState, TValue>> thunk)
        {
            this.thunk = thunk;
        }

        public State<TState, TResult> Map<TResult>(Func<TValue, TResult> map)
            => new(s => 
            {
                var (s2, v) = thunk(s);

                return (s2, map(v));
            });

        public State<TState, Func<T2, TResult>> PartialMap<T2, TResult>(Func<TValue, T2, TResult> map)
            => Map(map.Curry());

        public State<TState, TValue> Modify(Func<TState, TState> modify)
            => new(s => 
            {
                var (s2, v) = thunk(s);

                return (modify(s2), v);
            });

        public State<TState, TResult> AndThen<TResult>(Func<TValue, State<TState, TResult>> bind)
            => new(s => 
            {
                var (s2, v) = thunk(s);

                return bind(v).thunk(s2);
            });

        public State<TState, TResult> AndThen<TElement, TResult>(Func<TValue, State<TState, TElement>> bind, Func<TValue, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public StateResult<TState, TValue> Run(TState arg) => thunk(arg);

        public static State<TState, TValue> operator >>(State<TState, TValue> left, State<TState, TValue> right)
            => left.AndThen(_ => right);

        public static State<TState, TValue> operator >>(State<TState, TValue> left, State<TState, Unit> right)
            => left.AndThen(v => right.Map(_ => v));

        public static implicit operator State<TState, TValue>(TValue value) => State<TState>.Lift(value);
    }
}
