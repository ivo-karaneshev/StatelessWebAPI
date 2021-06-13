using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StatelessWebAPI.Caching.Services;

namespace StatelessWebAPI.Caching
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddCacheServices(this IServiceCollection services)
        {
            // Add connection manager
            services.AddSingleton<ConnectionManager>();

            return services;
        }

        public static IApplicationBuilder InitializeCache(this IApplicationBuilder app)
        {
            // Initialize Redis connection on startup to improve performance
            var connectionManager = app.ApplicationServices.GetRequiredService<ConnectionManager>();
            connectionManager.InitializeRedisConnection();

            return app;
        }
    }
}