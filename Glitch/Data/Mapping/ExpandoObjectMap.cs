using System.Data;
using System.Dynamic;

namespace Glitch.Data.Mapping
{
    internal class ExpandoObjectMap : ITypeMap
    {
        public Type ElementType => typeof(ExpandoObject);

        public bool CanMaterialize(IDataRecord record) => true;

        public object Materialize(IDataRecord record)
        {
            var obj = new ExpandoObject();
            var dict = obj as IDictionary<string, object>;

            for (int i = 0; i < record.FieldCount; ++i)
            {
                dict.Add(record.GetName(i), record.GetValue(i));
            }

            return obj;
        }
    }
}
