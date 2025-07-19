namespace Glitch.Functional.Parsing
{

    public partial class Parser<T>
    {
        public Parser<IEnumerable<T>> Times(int count) => new(input =>
        {
            
        });
    }
}
