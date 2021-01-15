using Desorganizze.Application.Commands.Login;
using Desorganizze.Application.Commands.Login.Handler;
using Desorganizze.Application.Commands.Users;
using Desorganizze.Application.Commands.Users.Handlers;
using Desorganizze.Application.Commands.Wallets;
using Desorganizze.Application.Commands.Wallets.Handlers;
using Desorganizze.Domain.Repositories;
using Desorganizze.Infra.CQRS.Commands;
using Desorganizze.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Desorganizze.Application.Commands
{
    public static class RegisterCommandHandlers
    {
        public static IServiceCollection RegisterCommandHandlersDependencies(
            this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<ICommandHandler<RegisterUser>, RegisterUserHandler>();
            services.AddTransient<ICommandHandler<AuthenticateCommand>, AuthenticateCommandHandler>();
            services.AddTransient<ICommandHandler<CreateAccount>, CreateAccountHandler>();
            services.AddTransient<ICommandHandler<CreateTransaction>, CreateTransactionHandler>();
            services.AddTransient<ICommandHandler<TransferBetweenAccounts>, TransferBetweenAccountsHandler>();
            return services;
        }
    }
}
