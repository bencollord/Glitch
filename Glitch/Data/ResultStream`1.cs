using Glitch.Data.Mapping;
using System.Data;
using System.Diagnostics;

namespace Glitch.Data;

public class ResultStream<T> : IDisposable
{
    private readonly ResultStream inner;

    internal ResultStream(IDataReader reader, ITypeMap typeMap)
        : this(new ResultStream(reader, typeMap)) { }

    internal ResultStream(ResultStream inner)
    {
        Debug.Assert(inner.ElementType.IsAssignableTo(typeof(T)), $"Invalid element types. From: {inner.ElementType}, To: {typeof(T)}");
        
        this.inner = inner;
    }

    public T Current => (T)inner.Current;

    public Type ElementType => inner.ElementType;

    public bool Read() => inner.Read();

    public void Close() => Dispose();

    public void Dispose() => inner.Dispose();

    public IList<T> ReadAll()
    {
        IEnumerable<T> Iterator()
        {
            while (Read())
            {
                yield return Current;
            }
        }

        return Iterator().ToList();
    }

    public static implicit operator ResultStream(ResultStream<T> typed) => typed.inner;

    public static explicit operator ResultStream<T>(ResultStream untyped) => untyped.Cast<T>();
}
