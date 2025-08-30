namespace Glitch.Functional
{
    internal class MapEnvIO<T> : IO<T>
    {
        private IO<T> source;
        private Func<IOEnv, IOEnv> map;

        internal MapEnvIO(IO<T> source, Func<IOEnv, IOEnv> map)
        {
            this.source = source;
            this.map = map;
        }

        public override T Run(IOEnv input) => source.Run(map(input));
    }
}