using Glitch.Data.Mapping;
using System.Data;

namespace Glitch.Data
{
    public class ResultStream : IDisposable
    {
        private readonly IDataReader reader;
        private readonly ITypeMap typeMap;
        private object? current;

        internal ResultStream(IDataReader reader, ITypeMap typeMap)
        {
            this.reader  = reader;
            this.typeMap = typeMap;
            current = default;
        }

        public virtual object Current => current = current ?? typeMap.Materialize(reader);

        public virtual Type ElementType => typeMap.ElementType;

        public bool Read() => reader.Read();

        public void Close() => Dispose();

        public void Dispose() => reader.Dispose();

        public ResultStream<T> Cast<T>() => new(this);
    }
}
