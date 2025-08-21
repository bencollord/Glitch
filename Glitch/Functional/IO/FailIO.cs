namespace Glitch.Functional
{
    internal class FailIO<TEnv, T> : IO<TEnv, T>
    {
        private Error error;

        internal FailIO(Error error)
        {
            this.error = error;
        }

        public override T Run(TEnv input) => error.Throw<T>();
    }
}