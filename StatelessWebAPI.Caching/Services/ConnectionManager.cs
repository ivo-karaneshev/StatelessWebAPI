using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace StatelessWebAPI.Caching.Services
{
    internal class ConnectionManager
    {
        private static ConnectionMultiplexer _connection;
        private readonly string _connectionString;

        public ConnectionManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RedisConnection");
        }

        public void InitializeRedisConnection()
        {
            if (_connection == null)
            {
                _connection = ConnectionMultiplexer.Connect(_connectionString);
            }
        }

        public ConnectionMultiplexer Connection => _connection;
    }
}