using FluentValidation;
using Meteorite.Api.Interfaces;
using Meteorite.Api.Models;
using Meteorite.Api.Services;
using Meteorite.Api.Validators;
using Meteorite.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System;
using System.Reflection;

namespace Meteorite.Api.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddResponseCaching();

            //Db
            var connection = configuration?.GetSection("ConnectionStrings")?.GetSection("DefaultConnection")?.Value;
            services.AddDbContext<MeteoriteContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IMeteoriteRepository, MeteoriteRepository>();

            // Validators
            services.AddScoped<IValidator<MeteoriteFilter>, MeteoriteFilterValidator>();
            services.AddFluentValidationAutoValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Meteorite API Documentation",
                    Version = "v1",
                    Description = "Patient API allows you to create, search, update and delete patients",
                    Contact = new OpenApiContact()
                    {
                        Name = "Aliaksei",
                        Email = "alexrusakovich1@gmail.com",
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins(configuration.GetSection("AllowedHosts").Value?.Split(';'))
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddEndpointsApiExplorer();

            return services;
        }

        public static WebApplication CreateDatabaseIfNotExist(this WebApplication app)
        {
            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MeteoriteContext>();
                context.Database.EnsureCreated();
            }

            return app;
        }
    }
}
