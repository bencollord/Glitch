using Glitch.Functional.Errors;

namespace Glitch.Functional
{
    internal class FailIO<T> : IO<T>
    {
        private Error error;

        internal FailIO(Error error)
        {
            this.error = error;
        }

        protected override Task<T> RunIOAsync(IOEnv env) => error.Throw<Task<T>>();
    }
}