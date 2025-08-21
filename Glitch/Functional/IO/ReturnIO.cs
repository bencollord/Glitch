namespace Glitch.Functional
{
    internal class ReturnIO<TEnv, T> : IO<TEnv, T>
    {
        private T value;

        internal ReturnIO(T value)
        {
            this.value = value;
        }

        public override T Run(TEnv input) => value;
    }
}