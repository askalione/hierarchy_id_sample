using Npgsql;
using System.Data;

namespace HierarchyIdSample.DataAccess
{
    internal class AppDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public AppDbConnectionFactory(string connectionString)
        {
            Ensure.NotEmpty(connectionString, nameof(connectionString));

            _connectionString = connectionString;
        }

        public IDbConnection CreateDbConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
