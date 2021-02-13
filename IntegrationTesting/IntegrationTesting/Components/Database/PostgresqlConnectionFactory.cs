using System;
using System.Threading.Tasks;
using Npgsql;

namespace IntegrationTesting.Components.Database
{
    public class PostgresqlConnectionFactory : IAsyncDisposable
    {
        public const string _connBase = "Server=127.0.0.1;Port=5666;User Id=postgres;Password=password;";
        public const string _db = "test_db";
        public static readonly string _conn = $"{_connBase};Database={_db}";

        private readonly string _connectionString;

        public PostgresqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private NpgsqlConnection _connection;

        public async Task<NpgsqlConnection> Create()
        {
            // not async safe
            if (_connection != null)
                return _connection;

            _connection = new(_connectionString);
            await _connection.OpenAsync();
            return _connection;
        }

        public ValueTask DisposeAsync() => _connection.DisposeAsync();
    }
}