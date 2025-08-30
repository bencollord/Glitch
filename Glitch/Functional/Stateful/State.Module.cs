namespace Glitch.Functional
{
    public static partial class State
    {
        public static IStateful<S, T> Return<S, T>(T value) => Lift((S state) => (state, value));

        public static IStateful<S, S> Get<S>() => Lift((S state) => (state, state));

        public static IStateful<S, T> Gets<S, T>(Func<S, T> function) => Lift((S state) => (state, function(state)));

        public static IStateful<S, Unit> Put<S>(S state) => Modify<S>(_ => state);

        public static IStateful<S, Unit> Modify<S>(Func<S, S> modify) => Lift((S state) => (modify(state), Nothing));

        public static IStateful<S, T> Lift<S, T>(Func<S, (S, T)> runner) => Lift<S, T>(s => runner(s));

        public static IStateful<S, T> Lift<S, T>(Func<S, StateResult<S, T>> runner) => new StateRunnerAdapter<S, T>(runner);

        private class StateRunnerAdapter<S, T> : IStateful<S, T>
        {
            private Func<S, StateResult<S, T>> runner;

            internal StateRunnerAdapter(Func<S, StateResult<S, T>> runner)
            {
                this.runner = runner;
            }

            public StateResult<S, T> Run(S state) => runner(state);

            public static implicit operator StateRunnerAdapter<S, T>(Func<S, StateResult<S, T>> runner) => new(runner);
        }
    }
}
