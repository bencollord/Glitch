using Glitch.Functional.Results;

namespace Glitch.Functional
{
    internal class FailIO<T> : IO<T>
    {
        private Error error;

        internal FailIO(Error error)
        {
            this.error = error;
        }

        public override T Run(IOEnv env) => error.Throw<T>();
    }
}