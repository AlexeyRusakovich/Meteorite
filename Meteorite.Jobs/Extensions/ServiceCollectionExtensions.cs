using Coravel;
using MeteoritDataSynchronizationJob;
using Meteorite.Jobs.Interfaces;
using Meteorite.Jobs.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Meteorite.Jobs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScheduler();
            services.AddSingleton<IMeteoriteCache, MeteoriteDbCache>();
            services.AddSingleton<IMeteoriteDataLoader, MeteoriteDataLoader>();
            services.AddSingleton<IMeteoriteRepository, MeteoriteRepository>();
            services.AddTransient<MeteoritesDataSynchronizationJob>();

            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));

            return services;
        }
    }
}
