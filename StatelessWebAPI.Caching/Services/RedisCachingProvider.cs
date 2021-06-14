using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace StatelessWebAPI.Caching.Services
{
    public class RedisCachingProvider : ICachingProvider
    {
        private readonly ConnectionManager _connectionManager;

        private IDatabase _db => _connectionManager.Database;
        private TimeSpan _defaultExpirationTime;

        public RedisCachingProvider(
            ConnectionManager connectionManager,
            IConfiguration configuration)
        {
            _connectionManager = connectionManager;

            // Set default expiration
            // Will throw an exception if config is missing or invalid
            var expiration = int.Parse(configuration["CacheDefaultExpirationMinutes"]);
            _defaultExpirationTime = TimeSpan.FromMinutes(expiration);
        }

        public Task SetValueAsync<T>(string key, T value)
        {
            return _db.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            var result = await _db.StringGetAsync(key);
            if (result.IsNullOrEmpty)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public Task InvalidateAsync(string key)
        {
            return _db.KeyDeleteAsync(key);
        }

        public Task SetExpirationAsync(string key, TimeSpan? expiration = null)
        {
            if (expiration == null)
            {
                expiration = _defaultExpirationTime;
            }

            return _db.KeyExpireAsync(key, expiration);
        }
    }
}