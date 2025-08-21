namespace Glitch.Functional
{
    internal class MapEnvIO<TNewEnv, TEnv, T> : IO<TNewEnv, T>
    {
        private IO<TEnv, T> source;
        private Func<TNewEnv, TEnv> map;

        internal MapEnvIO(IO<TEnv, T> source, Func<TNewEnv, TEnv> map)
        {
            this.source = source;
            this.map = map;
        }

        public override T Run(TNewEnv input) => source.Run(map(input));
    }
}