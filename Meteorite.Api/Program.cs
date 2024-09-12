using Meteorite.Api.Extensions;

namespace Meteorite.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var configuration = builder.Configuration;
            builder.Services.ConfigureServices(configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapControllers();

            app.CreateDatabaseIfNotExist();

            app.UseResponseCaching();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigin");

            app.Run();
        }
    }
}
