using Microsoft.Extensions.DependencyInjection;
using Meteorite.Jobs.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog.AspNetCore;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Serilog;

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

            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

            var host = builder.Build();
            
            RunScheduler(host, configuration);

            await host.RunAsync();
            Log.Logger.Information("Meteorite data syncronization job has started");
        }

        private static void RunScheduler(IHost host, IConfiguration configuration)
        {
            var meteoritesDataSynchronizationJobCron = configuration
                .GetSection("CronExpressions")
                .GetSection("MeteoritesDataSynchronizationJobCron").Value;

            host.Services
            .UseScheduler(scheduler =>
            {
                scheduler.Schedule<MeteoritesDataSynchronizationJob>()
                    .Cron(meteoritesDataSynchronizationJobCron)
                    .Zoned(TimeZoneInfo.Local);
            })
            .OnError(exception => 
            {
                exception.HandleException();
                throw exception;
            });
        }
    }
}