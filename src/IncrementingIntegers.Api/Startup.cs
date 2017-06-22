using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IncrementingIntegers.Logic.Extensions;
using IncrementingIntegers.Common.Configurations;
using IncrementingIntegers.Api.Mappings;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using IncrementingIntegers.DataAccess.Extensions;

namespace IncrementingIntegers.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Configurations
            services.Configure<StorageOptions>(Configuration.GetSection("StorageOptions"));

            // Add Layers
            services.AddLogicLayer();
            services.AddDataAccessLayer();

            // Setup Mappings
            services.AddAutoMapper(typeof(MappingProfile));

            // Add Cors Policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpa",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })

            // Add Formatters
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = Configuration["Formatters:DateFormat"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowSpa");

            app.UseExceptionHandler(
                appBuilder => appBuilder.Run(
                    async context =>
                    {
                        int statusCode = (int)HttpStatusCode.InternalServerError;
                        string message = "An error has occurred on the server. Please try again later";

                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsync(message);
                    })
                );

            app.UseMvc();
        }
    }
}
