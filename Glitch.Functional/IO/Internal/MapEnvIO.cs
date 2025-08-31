

namespace Glitch.Functional
{
    internal class MapEnvIO<T> : IO<T>
    {
        private readonly IO<T> source;
        private readonly Func<IOEnv, IOEnv> mapEnv;

        internal MapEnvIO(IO<T> source, Func<IOEnv, IOEnv> mapEnv)
        {
            this.source = source;
            this.mapEnv = mapEnv;
        }

        protected override async Task<T> RunIOAsync(IOEnv env)
        {
            return await source.RunAsync(mapEnv(env))
                               .ConfigureAwait(false);
        }
    }
}