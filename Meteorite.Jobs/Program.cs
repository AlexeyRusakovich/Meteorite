using Microsoft.Extensions.DependencyInjection;
using Meteorite.Jobs.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Coravel;

namespace MeteoritDataSynchronizationJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            builder.Services.Configure(configuration);

            var host = builder.Build();
            RunScheduler(host, configuration);

            await host.RunAsync();
        }

        private static void RunScheduler(IHost host, IConfiguration configuration)
        {
            var meteoritesDataSynchronizationJobCron = configuration
                .GetSection("CronExpressions")
                .GetSection("MeteoritesDataSynchronizationJobCron").Value;

            host.Services.UseScheduler(scheduler =>
            {
                scheduler.Schedule<MeteoritesDataSynchronizationJob>()
                    .Cron(meteoritesDataSynchronizationJobCron)
                    .Zoned(TimeZoneInfo.Local);
            })
            .OnError(exception => throw exception);
        }
    }
}