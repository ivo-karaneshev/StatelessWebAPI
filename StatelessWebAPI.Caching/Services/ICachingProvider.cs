using System.Threading.Tasks;

namespace StatelessWebAPI.Caching.Services
{
    public interface ICachingProvider
    {
        /// <summary>
        /// Caches value by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetValueAsync<T>(string key, T value);

        /// <summary>
        /// Gets cached value by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>Value or default if not found.</returns>
        Task<T> GetValueAsync<T>(string key);

        /// <summary>
        /// Deletes cached value by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task InvalidateAsync(string key);
    }
}