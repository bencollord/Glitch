namespace Glitch.Functional
{
    internal class LiftIO<TEnv, T> : IO<TEnv, T>
    {
        private Func<TEnv, T> runIO;

        internal LiftIO(Func<TEnv, T> runIO)
        {
            this.runIO = runIO;
        }

        public override T Run(TEnv input) => runIO(input);
    }
}