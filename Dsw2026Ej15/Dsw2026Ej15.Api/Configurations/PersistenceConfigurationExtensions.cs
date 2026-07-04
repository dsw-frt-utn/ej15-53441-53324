using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api.Configurations
{
    public static class PersistenceConfigurationExtensions
    {
        public static IServiceCollection AddApplicationPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<Dsw2026Ej15DbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }

        public static IHost LoadSpecialityData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<Dsw2026Ej15DbContext>();
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
            context.SeedworkSpecialities(jsonPath);
            return host;
        }
    }
}
