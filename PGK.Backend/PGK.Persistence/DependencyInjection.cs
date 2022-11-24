using Microsoft.Extensions.DependencyInjection;
using PGK.Application.Interfaces;

namespace PGK.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection
            services)
        {
            services.MySqlConnection();

            services.AddScoped<IPGKDbContext>(provider => provider.GetService<PGKDbContext>());

            return services;
        }
    }
}
