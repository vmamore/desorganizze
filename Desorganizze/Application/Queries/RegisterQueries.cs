using Desorganizze.Application.Queries.Users.Parameters;
using Desorganizze.Application.Queries.Users.Processors;
using Desorganizze.Application.Queries.Users.ReadModel;
using Desorganizze.Application.Queries.Wallets.Parameters;
using Desorganizze.Application.Queries.Wallets.Processors;
using Desorganizze.Application.Queries.Wallets.ReadModel;
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
            service.AddTransient<IQueryHandler<GetAllAccountsFromUser, IEnumerable<AccountFromWalletDto>>, WalletsQueryHandler>();
            service.AddTransient<IQueryHandler<GetTransactionsFromUser, IEnumerable<TransactionQueryDto>>, WalletsQueryHandler>();
            service.AddTransient<IQueryHandler<GetWalletWithAccountsFromUser, WalletWithAcountDto>, WalletsQueryHandler>();
            return service;
        }
    }
}
