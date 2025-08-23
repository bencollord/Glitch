namespace Glitch.Functional
{
    internal class ReturnIO<T> : IO<T>
    {
        private T value;

        internal ReturnIO(T value)
        {
            this.value = value;
        }

        public override T Run(IOEnv env) => value;
    }
}