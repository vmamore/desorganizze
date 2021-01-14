using Desorganizze.Application.Queries.Users.Parameters;
using Desorganizze.Application.Queries.Users.Processors;
using Desorganizze.Application.Queries.Users.ReadModel;
using Desorganizze.Infra.CQRS.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Desorganizze.Application.Queries
{
    public static class RegisterQueries
    {
        public static IServiceCollection RegisterQueriesApplicationDependencies(
            this IServiceCollection service)
        {
            service.AddScoped<IQueryHandler<GetUserById, UserDtoItem>, UserQueryHandler>();
            service.AddTransient<IQueryHandler<GetAllUsers, IEnumerable<UserDtoItem>>, UserQueryHandler>();

            return service;
        }
    }
}
