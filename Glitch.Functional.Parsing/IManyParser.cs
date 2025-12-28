namespace Glitch.Functional.Parsing;

public interface IManyParser<TToken, T, out TCollection> : IParser<T, TCollection>
    where TCollection : IEnumerable<T>
{
    IParser<TToken, TCollection> AtLeast(int times);
    IParser<TToken, TCollection> ZeroOrMoreTimes();
    IParser<TToken, TCollection> Times(int count);
}
