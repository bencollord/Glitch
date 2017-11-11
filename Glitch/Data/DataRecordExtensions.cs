using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class DataRecordExtensions
    {
        public static bool HasField(this IDataRecord record, string columnName)
        {
            Guard.NotNull(record, nameof(record));
            return record.GetFieldNames().Any(columnName.Equals);
        }

        public static IEnumerable<string> GetFieldNames(this IDataRecord record)
        {
            Guard.NotNull(record, nameof(record));

            for (int i = 0; i < record.FieldCount; ++i)
            {
                yield return record.GetName(i);
            }
        }
    }
}
