using Desorganizze.Infra.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Desorganizze.Infra.Extensions
{
    public static class MigrationRunnerConfiguration
    {
        public static IServiceCollection AddMigrationRunner(this IServiceCollection services, string connectionString)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                    rb.AddPostgres10_0()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(AddUserTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }
    }
}
