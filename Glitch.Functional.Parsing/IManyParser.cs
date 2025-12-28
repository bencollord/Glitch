namespace Glitch.Functional.Parsing;

public interface IManyParser<TToken, out T, out TCollection> : IParser<TToken, TCollection>
    where TCollection : IEnumerable<T>
{
    IParser<TToken, TCollection> AtLeast(int times);
    IParser<TToken, TCollection> AtMost(int times);
    IParser<TToken, TCollection> ZeroOrMoreTimes();
    IParser<TToken, TCollection> Times(int count);
}
