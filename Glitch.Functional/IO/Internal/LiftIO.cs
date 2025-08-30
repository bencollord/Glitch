namespace Glitch.Functional
{
    internal class LiftIO<T> : IO<T>
    {
        private Func<IOEnv, T> runIO;

        internal LiftIO(Func<IOEnv, T> runIO)
        {
            this.runIO = runIO;
        }

        public override T Run(IOEnv env) => runIO(env);
    }
}