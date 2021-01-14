using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Desorganizze.Infra.CQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly DependencyResolver _dependencyResolver;
        public CommandDispatcher(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }
        public async Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var handler = this._dependencyResolver.Resolve<ICommandHandler<TCommand>>();

            return await handler.ExecuteAsync(command);
        }
    }
}
