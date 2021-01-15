using Desorganizze.Application.Commands.Login;
using Desorganizze.Application.Commands.Login.Handler;
using Desorganizze.Application.Commands.Users;
using Desorganizze.Application.Commands.Users.Handlers;
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
            return services;
        }
    }
}
