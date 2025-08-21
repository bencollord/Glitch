namespace Glitch.Functional
{
    internal class GuardIO<TEnv, T> : IO<TEnv, T>
    {
        private IO<TEnv, T> source;
        private Func<T, bool> predicate;
        private Func<T, Error> error;

        public GuardIO(IO<TEnv, T> source, Func<T, bool> predicate, Func<T, Error> error)
        {
            this.source = source;
            this.predicate = predicate;
            this.error = error;
        }

        public override T Run(TEnv input)
        {
            var result = source.Run(input);

            if (!predicate(result))
            {
                error(result).Throw();
            }

            return result;
        }
    }
}