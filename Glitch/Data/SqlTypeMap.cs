using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class SqlTypeMap
    {
        private static readonly Dictionary<SqlDbType, Type> Map = new Dictionary<SqlDbType, Type>
        {
            { SqlDbType.BigInt, typeof(long) },
            { SqlDbType.Binary, typeof(byte[]) },
            { SqlDbType.Bit, typeof(bool) },
            { SqlDbType.Char, typeof(string) },
            { SqlDbType.Date, typeof(DateTime) },
            { SqlDbType.DateTime, typeof(DateTime) },
            { SqlDbType.DateTime2, typeof(DateTime) },
            { SqlDbType.DateTimeOffset, typeof(DateTimeOffset) },
            { SqlDbType.Decimal, typeof(decimal) },
            { SqlDbType.Float, typeof(double) },
            { SqlDbType.Image, typeof(byte[]) },
            { SqlDbType.Int, typeof(int) },
            { SqlDbType.Money, typeof(decimal) },
            { SqlDbType.NChar, typeof(string) },
            { SqlDbType.NText, typeof(string) },
            { SqlDbType.NVarChar, typeof(string) },
            { SqlDbType.Real, typeof(float) },
            { SqlDbType.SmallDateTime, typeof(DateTime) },
            { SqlDbType.SmallInt, typeof(short) },
            { SqlDbType.SmallMoney, typeof(decimal) },
            { SqlDbType.Structured, typeof(object) },
            { SqlDbType.Text, typeof(string) },
            { SqlDbType.Time, typeof(DateTime) },
            { SqlDbType.Timestamp, typeof(byte[]) },
            { SqlDbType.TinyInt, typeof(byte) },
            { SqlDbType.Udt, typeof(object) },
            { SqlDbType.UniqueIdentifier, typeof(Guid) },
            { SqlDbType.VarBinary, typeof(byte[]) },
            { SqlDbType.VarChar, typeof(string) },
            { SqlDbType.Variant, typeof(object) },
            { SqlDbType.Xml, typeof(string) }
        };

        public static Type GetClrType(SqlDbType dbType) => GetClrType(dbType, false);

        public static Type GetClrType(SqlDbType dbType, bool isNullable)
        {
            Type clrType = Map[dbType];

            if (isNullable && clrType.IsValueType)
            {
                return typeof(Nullable<>).MakeGenericType(clrType);
            }

            return clrType;
        }

        public static IEnumerable<SqlDbType> GetSqlTypes(Type clrType)
        {
            Type type = clrType;

            if (clrType.IsGenericType && clrType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                type = clrType.GetGenericArguments().Single();
            }
            if (!Map.Values.Contains(clrType))
            {
                type = typeof(object);
            }

            return Map.Where(entry => entry.Value.Equals(type)).Select(entry => entry.Key);
        }
    }
}
