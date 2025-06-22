using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Data
{
    public static class DbConnectionExtensions
    {
        public static void EnsureOpen(this DbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public static DataTable Query(this DbConnection connection, string sql)
        {
            connection.EnsureOpen();

            using (var command = connection.CreateCommand(sql))
            {
                return command.ExecuteTable();
            }
        }

        public static IEnumerable<T> Query<T>(this DbConnection connection, string sql)
        {
            connection.EnsureOpen();

            using var command = connection.CreateCommand(sql);

            return command.Execute<T>().ReadAll();
        }

        public static IEnumerable Query(this DbConnection connection, Type type, string sql)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            using var command = connection.CreateCommand(sql);
            return command.Execute(type).Cast<object>().ReadAll();
        }

        public static DbCommand CreateCommand(this DbConnection connection, string sql)
        {
            var command = connection.CreateCommand();

            command.CommandText = sql;

            return command;
        }

        public static IEnumerable<ColumnInfo> GetColumns(this DbConnection connection, string tableName, Func<ColumnInfo, bool> predicate)
            => connection.GetColumns(tableName).Where(predicate);

        public static IEnumerable<ColumnInfo> GetColumns(this DbConnection connection, string tableName)
            => connection.Query($"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'")
                         .AsEnumerable()
                         .Select(ColumnInfo.FromDataRow);
    }
}
