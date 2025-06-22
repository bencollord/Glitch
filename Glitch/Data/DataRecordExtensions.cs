using Glitch.Functional;
using System.Data;

namespace Glitch.Data
{
    public static class DataRecordExtensions
    {
        /// <summary>
        /// Gets a record's value by name and casts it to the provided type.
        /// Returns null if the value is <see cref="DBNull"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="ordinal"></param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="fieldName"/> is not included in the data set.
        /// </exception>
        /// <returns></returns>
        public static T? GetValue<T>(this IDataRecord record, string fieldName)
            => record.GetValue<T>(record.GetOrdinal(fieldName));

        /// <summary>
        /// Gets a record's value by ordinal and casts it to the provided type.
        /// Returns null if the value is <see cref="DBNull"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="ordinal"></param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the ordinal is less than zero or greater than the record's field count
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// If the value cannot be cast to <typeparamref name="T"/>.
        /// </exception>
        /// <returns></returns>
        public static T? GetValue<T>(this IDataRecord record, int ordinal)
        {
            ArgumentNullException.ThrowIfNull(record);

            if (record.IsDBNull(ordinal))
            {
                return default;
            }

            return (T)record.GetValue(ordinal);
        }

        /// <summary>
        /// Gets a record's value by name and casts it to the provided type if 
        /// the provided name is included in the data set and the field is not
        /// <see cref="DBNull"/>. Otherwise, returns <see cref="Option{T}.None"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="ordinal"></param>
        /// <exception cref="InvalidCastException">
        /// If the value cannot be cast to <typeparamref name="T"/>.
        /// </exception>
        /// <returns></returns>
        public static Option<T> TryGetValue<T>(this IDataRecord record, string name)
            => Try(() => record.GetOrdinal(name))
                   .Run().OrNone()
                   .AndThen(record.TryGetValue<T>);

        /// <summary>
        /// Gets a record's value by ordinal and casts it to the provided type if 
        /// the provided ordinal is valid for the record and the field is not
        /// <see cref="DBNull"/>. Otherwise, returns <see cref="Option{T}.None"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="ordinal"></param>
        /// <exception cref="InvalidCastException">
        /// If the value cannot be cast to <typeparamref name="T"/>.
        /// </exception>
        /// <returns></returns>
        public static Option<T> TryGetValue<T>(this IDataRecord record, int ordinal)
            => from r in Some(record)
               where ordinal >= 0 
                  && ordinal < record.FieldCount
               from v in Maybe(r.GetValue<T>(ordinal))
               select v;

        /// <summary>
        /// Enumerates the field names included in the <paramref name="record"/>.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetFieldNames(this IDataRecord record)
        {
            ArgumentNullException.ThrowIfNull(record);

            for (int i = 0; i < record.FieldCount; ++i)
            {
                yield return record.GetName(i);
            }
        }
    }
}
