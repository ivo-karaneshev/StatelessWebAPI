using System;
using System.Threading.Tasks;

namespace StatelessWebAPI.Caching.Services
{
    public interface ICachingProvider
    {
        Task SetValueAsync<T>(string key, T value);

        Task<T> GetValueAsync<T>(string key);

        Task InvalidateAsync(string key);

        Task SetExpirationAsync(string key, TimeSpan? expiration = null);
    }
}