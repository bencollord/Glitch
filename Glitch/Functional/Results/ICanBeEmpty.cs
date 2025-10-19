namespace Glitch.Functional.Results
{
    public interface ICanBeEmpty<T>
    {
        public static abstract T Empty { get; }
    }
}
