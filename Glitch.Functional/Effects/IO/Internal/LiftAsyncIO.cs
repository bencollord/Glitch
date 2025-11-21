namespace Glitch.Functional.Effects
{
    internal class LiftAsyncIO<T> : IO<T>
    {
        private Func<IOEnv, Task<T>> runAsyncIO;

        internal LiftAsyncIO(Func<IOEnv, Task<T>> runAsyncIO)
        {
            this.runAsyncIO = runAsyncIO;
        }

        protected override async Task<T> RunIOAsync(IOEnv env) => await runAsyncIO(env).ConfigureAwait(false);
    }
}