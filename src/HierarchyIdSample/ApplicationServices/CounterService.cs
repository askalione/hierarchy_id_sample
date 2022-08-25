using Dapper;

namespace HierarchyIdSample.ApplicationServices
{
    internal class CounterService : ICounterService
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CounterService(IDbConnectionFactory dbConnectionFactory)
        {
            Ensure.NotNull(dbConnectionFactory, nameof(dbConnectionFactory));

            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> GetValueAsync(string name, CancellationToken cancellationToken = default)
        {
            string sql = $@"
                UPDATE counters
                SET value = value + 1
                WHERE name = @Name
                RETURNING value";

            using (var connection = _dbConnectionFactory.CreateDbConnection())
            {
                int? value = await connection.QueryFirstOrDefaultAsync<int?>(
                    new CommandDefinition(sql, new { Name = name }, cancellationToken: cancellationToken));
                if (value == null)
                {
                    throw new InvalidOperationException($"Counter not found (name={name}).");
                }

                return (int)value;
            }
        }
    }
}
