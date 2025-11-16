using Glitch.Functional.Core;

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

        protected override async Task<T> RunIOAsync(IOEnv env)
        {
            var result = await source.RunAsync(env).ConfigureAwait(false);

            if (!predicate(result))
            {
                error(result).Throw();
            }

            return result;
        }
    }
}