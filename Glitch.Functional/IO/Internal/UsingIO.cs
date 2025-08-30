namespace Glitch.Functional
{
    internal class UsingIO<T> : IO<T>
    {
        private IO<T> acquire;

        internal UsingIO(IO<T> acquire)
        {
            this.acquire = acquire;
        }

        public override T Run(IOEnv env)
        {
            var result = acquire.Run(env);

            env.Track(result);

            return result;
        }
    }
}