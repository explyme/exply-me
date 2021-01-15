using System;
using System.Data.SqlClient;

namespace ExplyMe.Infrastructure.Data
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        public string ReadConnectionString { get; }
        public string WriteConnectionString { get; }

        public SqlConnectionFactory(
            string readConnectionString,
            string writeConnectionString)
        {
            ReadConnectionString = readConnectionString ?? throw new ArgumentNullException(nameof(readConnectionString));
            WriteConnectionString = writeConnectionString ?? throw new ArgumentNullException(nameof(writeConnectionString));
        }

        public SqlConnection CreateReadConnection() => new SqlConnection(ReadConnectionString);

        public SqlConnection CreateWriteConnection() => new SqlConnection(WriteConnectionString);
    }
}
