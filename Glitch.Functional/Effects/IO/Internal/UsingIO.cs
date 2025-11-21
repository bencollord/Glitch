
namespace Glitch.Functional.Effects
{
    internal class UsingIO<T> : IO<T>
    {
        private IO<T> acquire;

        internal UsingIO(IO<T> acquire)
        {
            this.acquire = acquire;
        }

        protected override async Task<T> RunIOAsync(IOEnv env)
        {
            var result = await acquire.RunAsync(env).ConfigureAwait(false);

            env.Track(result);

            return result;
        }
    }
}