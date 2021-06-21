namespace Desorganizze
{
    using Desorganizze.Application.Commands;
    using Desorganizze.Application.Queries;
    using Desorganizze.Infra.CQRS;
    using Desorganizze.Infra.Extensions;
    using FluentMigrator.Runner;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using System.Text.Json.Serialization;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {

            var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            Configuration = configurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = Configuration["ConnectionStrings:PgSql"];
            var redisConnectionString = Configuration["ConnectionStrings:Redis"];

            services.AddHealthChecks()
                .AddNpgSql(dbConnectionString);

            services.AddJwt();

            services.AddMigrationRunner(dbConnectionString);

            services.AddNHibernate(dbConnectionString);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.IgnoreNullValues = true;
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                        options.JsonSerializerOptions.MaxDepth = 0;
                    });

            services.AddSwagger();

            services
                .RegisterQueriesApplicationDependencies()
                .RegisterCommandHandlersDependencies()
                .RegisterInfrastructureCQRSDependencies();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner, ILogger<Startup> logger)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desorganizze API");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}
