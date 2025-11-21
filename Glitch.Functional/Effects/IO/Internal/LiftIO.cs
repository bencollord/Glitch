namespace Glitch.Functional.Effects
{
    internal class LiftIO<T> : IO<T>
    {
        private Func<IOEnv, T> runIO;

        internal LiftIO(Func<IOEnv, T> runIO)
        {
            this.runIO = runIO;
        }

        protected override Task<T> RunIOAsync(IOEnv env) => Task.FromResult(runIO(env));
    }
}