
namespace Glitch.Functional.Effects
{
    internal class ReturnAsyncIO<T> : IO<T>
    {
        private Task<T> value;

        internal ReturnAsyncIO(Task<T> value)
        {
            this.value = value;
        }

        protected override async Task<T> RunIOAsync(IOEnv env) => await value.ConfigureAwait(false);
    }
}