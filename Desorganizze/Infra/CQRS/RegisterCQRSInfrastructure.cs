using Desorganizze.Infra.CQRS.Commands;
using Desorganizze.Infra.CQRS.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Desorganizze.Infra.CQRS
{
    public static class RegisterCQRSInfrastructure
    {
        public static IServiceCollection RegisterInfrastructureCQRSDependencies(
            this IServiceCollection services)
        {
            services.AddSingleton<DependencyResolver>();

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IQueryProcessor, QueryProcessor>();

            return services;
        }
    }
}
