using Glitch.Functional.Results;

namespace Glitch.Functional
{
    internal class GuardIO<T> : IO<T>
    {
        private IO<T> source;
        private Func<T, bool> predicate;
        private Func<T, Error> error;

        public GuardIO(IO<T> source, Func<T, bool> predicate, Func<T, Error> error)
        {
            this.source = source;
            this.predicate = predicate;
            this.error = error;
        }

        public override T Run(IOEnv env)
        {
            var result = source.Run(env);

            if (!predicate(result))
            {
                error(result).Throw();
            }

            return result;
        }
    }
}