namespace Glitch.Functional
{
    public interface ICanBeEmpty<T>
    {
        public static abstract T Empty { get; }
    }
}
