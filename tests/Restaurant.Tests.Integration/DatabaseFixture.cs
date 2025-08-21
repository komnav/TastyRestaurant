using Npgsql;
using Testcontainers.PostgreSql;

namespace Restaurant.Tests.Integration
{
    public class DatabaseFixture
    {
        public const string _initialSnapshotName = "RestaurantDbInContainer";

        private PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase(_initialSnapshotName)
            .WithUsername("postgres")
            .WithPassword("admin")
            .WithPortBinding(5435, 5432)
            .Build();

        public async Task Startasync()
        {
            await _dbContainer.StartAsync();
        }

        public async Task ResetDatabase()
        {
            await using var connection = new NpgsqlConnection(_dbContainer.GetConnectionString().Replace($"Database={_initialSnapshotName}", "Database=postgres"));
            await connection.OpenAsync();

            var sqlToDeleteDb = $"DROP DATABASE IF EXISTS \"{_initialSnapshotName}\" WITH (FORCE)";

            await using (var cmd = new NpgsqlCommand($"{sqlToDeleteDb}", connection))
            {
                var result = await cmd.ExecuteNonQueryAsync();
                Console.WriteLine(result);
            }
            NpgsqlConnection.ClearAllPools();
        }

        public string GetConnectionString()
        {
            return _dbContainer.GetConnectionString();
        }

        public async Task RestartAsync()
        {
            await _dbContainer.DisposeAsync();

            _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase(_initialSnapshotName)
                .WithUsername("postgres")
                .WithPassword("admin")
                .WithPortBinding(5433, 5432)
                .Build();
        }
    }
}