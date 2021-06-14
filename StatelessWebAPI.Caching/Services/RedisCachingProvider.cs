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
        private int _cacheExpirationTime;

        public RedisCachingProvider(
            ConnectionManager connectionManager,
            IConfiguration configuration)
        {
            _connectionManager = connectionManager;

            int.TryParse(configuration["CacheExpirationTimeMinutes"], out _cacheExpirationTime);
        }

        public async Task SetValueAsync<T>(string key, T value)
        {
            await _db.StringSetAsync(key, JsonConvert.SerializeObject(value));
            if (_cacheExpirationTime > 0)
            {
                await _db.KeyExpireAsync(key, TimeSpan.FromMinutes(_cacheExpirationTime));
            }
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
    }
}