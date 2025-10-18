namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        public sealed record Success<T>(T Value) 
            : Expected<T>(Result.Okay<T, Error>(Value)),
              Okay<T>;
    }
}
