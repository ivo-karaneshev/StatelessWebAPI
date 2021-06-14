using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatelessWebAPI.Data.Services;

namespace StatelessWebAPI.Data
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddDataServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add Db context
            services.AddDbContext<GameDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add data service
            services.AddScoped<IGameDataService, GameDataService>();

            return services;
        }

        public static IApplicationBuilder UseLocalDbMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}