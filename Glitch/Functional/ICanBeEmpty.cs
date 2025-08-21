namespace Glitch.Functional
{
    public interface ICanBeEmpty<T> where T : ICanBeEmpty<T>
    {
        public static abstract T Empty { get; }
    }
}
